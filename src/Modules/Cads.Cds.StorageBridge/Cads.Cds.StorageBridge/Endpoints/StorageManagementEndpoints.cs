using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Endpoints.Responses;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Configuration;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Crypto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Security.Cryptography;

namespace Cads.Cds.StorageBridge.Endpoints;

public static class StorageManagementEndpoints
{
    public static IEndpointRouteBuilder MapStorageBridgeStorageManagementEndpoints(this IEndpointRouteBuilder app)
    {
        var storageConfig = app.ServiceProvider.GetRequiredService<StorageBridgeStorageConfiguration>();

        if (!storageConfig.StorageManager.Enabled)
        {
            return app;
        }

        var group = app.MapGroup("/api/v1/storage/management")
            .RequireAuthorization(AuthenticationConstants.ApiKeyOrCognitoPolicy)
            .WithTags("StorageManagement");

        group.MapGet("/buckets", ListBuckets);
        group.MapGet("/buckets/{clientName}/objects", ListObjects);
        group.MapGet("/buckets/{clientName}/search", SearchKeys);
        group.MapGet("/buckets/{clientName}/object", GetObject);
        group.MapGet("/buckets/{clientName}/object/rows", GetObjectRows);
        group.MapPut("/buckets/{clientName}/object", PutObject);
        group.MapDelete("/buckets/{clientName}/object", DeleteObject);

        return app;
    }

    private static IResult ListBuckets(IServiceProvider services)
    {
        var buckets = new List<StorageBucketResponse>();

        AddBucket<CadsInternalClient>(services, buckets);
        AddBucket<CadsExternalClient>(services, buckets);

        return Results.Ok(buckets);
    }

    private static void AddBucket<T>(IServiceProvider services, List<StorageBucketResponse> buckets)
        where T : IStorageClient, new()
    {
        var manager = services.GetService<IStorageManager<T>>();

        if (manager is not null)
        {
            buckets.Add(new StorageBucketResponse(manager.ClientName, manager.BucketName));
        }
    }

    private static Task<IResult> ListObjects(
        string clientName,
        string? prefix,
        string? delimiter,
        int? maxKeys,
        string? continuationToken,
        string? pattern,
        string? patternMode,
        IServiceProvider services,
        CancellationToken cancellationToken) =>
        clientName switch
        {
            nameof(CadsInternalClient) => ListClientObjects<CadsInternalClient>(services, prefix, delimiter, maxKeys, continuationToken, pattern, patternMode, cancellationToken),
            nameof(CadsExternalClient) => ListClientObjects<CadsExternalClient>(services, prefix, delimiter, maxKeys, continuationToken, pattern, patternMode, cancellationToken),
            _ => Task.FromResult(UnknownClient(clientName))
        };

    private static async Task<IResult> ListClientObjects<T>(IServiceProvider services, string? prefix, string? delimiter, int? maxKeys, string? continuationToken, string? pattern, string? patternMode, CancellationToken cancellationToken)
        where T : IStorageClient, new()
    {
        Func<string, bool>? matches = null;

        if (!string.IsNullOrEmpty(pattern))
        {
            matches = StorageKeyMatcher.Create(pattern, patternMode);

            if (matches is null)
            {
                return Results.BadRequest($"Invalid pattern '{pattern}' for mode '{patternMode ?? "contains"}'");
            }
        }

        var manager = services.GetRequiredService<IStorageManager<T>>();

        var listing = await manager.ListObjectsAsync(
            prefix ?? string.Empty,
            delimiter,
            maxKeys ?? 1000,
            continuationToken,
            cancellationToken);

        if (matches is not null)
        {
            var basePrefix = prefix ?? string.Empty;

            listing = listing with
            {
                Folders = listing.Folders.Where(folder => matches(RelativeName(folder, basePrefix))).ToList(),
                Objects = listing.Objects.Where(item => matches(RelativeName(item.Key, basePrefix))).ToList()
            };
        }

        return Results.Ok(listing);
    }

    private static string RelativeName(string key, string prefix)
    {
        var relative = key.StartsWith(prefix, StringComparison.Ordinal) ? key[prefix.Length..] : key;

        return relative.TrimEnd('/');
    }

    private static Task<IResult> SearchKeys(
        string clientName,
        string pattern,
        string? prefix,
        IServiceProvider services,
        CancellationToken cancellationToken) =>
        clientName switch
        {
            nameof(CadsInternalClient) => SearchClientKeys<CadsInternalClient>(services, pattern, prefix, cancellationToken),
            nameof(CadsExternalClient) => SearchClientKeys<CadsExternalClient>(services, pattern, prefix, cancellationToken),
            _ => Task.FromResult(UnknownClient(clientName))
        };

    private static async Task<IResult> SearchClientKeys<T>(IServiceProvider services, string pattern, string? prefix, CancellationToken cancellationToken)
        where T : IStorageClient, new()
    {
        var manager = services.GetRequiredService<IStorageManager<T>>();

        var keys = await manager.SearchKeysAsync(pattern, prefix, cancellationToken);

        return Results.Ok(new StorageSearchResponse(keys));
    }

    private static Task<IResult> GetObject(
        string clientName,
        string key,
        IServiceProvider services,
        CancellationToken cancellationToken) =>
        clientName switch
        {
            nameof(CadsInternalClient) => GetClientObject<CadsInternalClient>(services, key, decryptCtsm: false, cancellationToken),
            nameof(CadsExternalClient) => GetClientObject<CadsExternalClient>(services, key, decryptCtsm: true, cancellationToken),
            _ => Task.FromResult(UnknownClient(clientName))
        };

    private static async Task<IResult> GetClientObject<T>(IServiceProvider services, string key, bool decryptCtsm, CancellationToken cancellationToken)
        where T : IStorageClient, new()
    {
        var manager = services.GetRequiredService<IStorageManager<T>>();

        try
        {
            var response = await manager.GetObjectResponseAsync(key, cancellationToken);
            var (stream, contentType) = DecodeObjectStream(services, key, decryptCtsm, response);

            return Results.Stream(stream, contentType);
        }
        catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return Results.NotFound();
        }
    }

    /// <summary>
    /// The object's readable stream — decrypted for external CTSM files, so
    /// callers always see plaintext content.
    /// </summary>
    private static (Stream Stream, string ContentType) DecodeObjectStream(
        IServiceProvider services,
        string key,
        bool decryptCtsm,
        GetObjectResponse response)
    {
        if (decryptCtsm && CtsmFilenameParser.TryParse(Path.GetFileName(key), out var ctsmFilename))
        {
            var salt = services.GetRequiredService<StorageBridgeStorageConfiguration>().StorageManager.Salt;
            var decryptor = AesCryptoTransform.CreateDecryptor(ctsmFilename!.DerivePassword(), salt);

            return (new CryptoStream(response.ResponseStream, decryptor, CryptoStreamMode.Read), "text/csv");
        }

        return (response.ResponseStream, response.Headers.ContentType ?? "application/octet-stream");
    }

    private const int MaxRowSliceCount = 1000;

    private static Task<IResult> GetObjectRows(
        string clientName,
        string key,
        int startRow,
        int rowCount,
        string delimiter,
        IServiceProvider services,
        CancellationToken cancellationToken) =>
        clientName switch
        {
            nameof(CadsInternalClient) => GetClientObjectRows<CadsInternalClient>(services, key, decryptCtsm: false, startRow, rowCount, delimiter, cancellationToken),
            nameof(CadsExternalClient) => GetClientObjectRows<CadsExternalClient>(services, key, decryptCtsm: true, startRow, rowCount, delimiter, cancellationToken),
            _ => Task.FromResult(UnknownClient(clientName))
        };

    private static async Task<IResult> GetClientObjectRows<T>(IServiceProvider services, string key, bool decryptCtsm, int startRow, int rowCount, string delimiter, CancellationToken cancellationToken)
        where T : IStorageClient, new()
    {
        if (startRow < 1)
        {
            return Results.BadRequest("startRow must be 1 or greater");
        }

        if (rowCount < 1 || rowCount > MaxRowSliceCount)
        {
            return Results.BadRequest($"rowCount must be between 1 and {MaxRowSliceCount}");
        }

        if (string.IsNullOrEmpty(delimiter))
        {
            return Results.BadRequest("delimiter is required");
        }

        var manager = services.GetRequiredService<IStorageManager<T>>();

        try
        {
            // Disposing before the body is drained aborts the storage download,
            // so completing the slice early stops pulling the rest of the object.
            using var response = await manager.GetObjectResponseAsync(key, cancellationToken);
            var stream = DecodeObjectStream(services, key, decryptCtsm, response).Stream;

            return Results.Ok(await StorageRowSliceReader.ReadAsync(stream, startRow, rowCount, delimiter, cancellationToken));
        }
        catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return Results.NotFound();
        }
        catch (InvalidDataException e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    private static Task<IResult> PutObject(
        string clientName,
        string key,
        HttpRequest request,
        IServiceProvider services,
        CancellationToken cancellationToken) =>
        clientName switch
        {
            nameof(CadsInternalClient) => PutClientObject<CadsInternalClient>(services, key, request, cancellationToken),
            nameof(CadsExternalClient) => PutClientObject<CadsExternalClient>(services, key, request, cancellationToken),
            _ => Task.FromResult(UnknownClient(clientName))
        };

    private static async Task<IResult> PutClientObject<T>(IServiceProvider services, string key, HttpRequest request, CancellationToken cancellationToken)
        where T : IStorageClient, new()
    {
        var manager = services.GetRequiredService<IStorageManager<T>>();

        using var content = new MemoryStream();
        await request.Body.CopyToAsync(content, cancellationToken);
        content.Position = 0;

        await manager.PutObjectAsync(key, content, request.ContentType, cancellationToken);

        return Results.NoContent();
    }

    private static Task<IResult> DeleteObject(
        string clientName,
        string key,
        IServiceProvider services,
        CancellationToken cancellationToken) =>
        clientName switch
        {
            nameof(CadsInternalClient) => DeleteClientObject<CadsInternalClient>(services, key, cancellationToken),
            nameof(CadsExternalClient) => DeleteClientObject<CadsExternalClient>(services, key, cancellationToken),
            _ => Task.FromResult(UnknownClient(clientName))
        };

    private static async Task<IResult> DeleteClientObject<T>(IServiceProvider services, string key, CancellationToken cancellationToken)
        where T : IStorageClient, new()
    {
        var manager = services.GetRequiredService<IStorageManager<T>>();

        await manager.DeleteObjectAsync(key, cancellationToken);

        return Results.NoContent();
    }

    private static IResult UnknownClient(string clientName) =>
        Results.NotFound($"No storage client named '{clientName}' is available");
}
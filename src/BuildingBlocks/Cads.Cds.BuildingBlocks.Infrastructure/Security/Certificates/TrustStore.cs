using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Security.Certificates;

[ExcludeFromCodeCoverage]
public static class TrustStore
{
    public static void AddCustomTrustStore(this IServiceCollection _)
    {
        var certificates = GetCertificates();
        AddCertificates(certificates);
    }

    private static List<string> GetCertificates()
    {
        return [.. Environment.GetEnvironmentVariables()
            .Cast<DictionaryEntry>()
            .Where(entry =>
                entry.Key.ToString()!.StartsWith("TRUSTSTORE")
                    && IsBase64String(entry.Value!.ToString() ?? ""))
            .Select(entry =>
            {
                var data = Convert.FromBase64String(entry.Value!.ToString() ?? "");
                return Encoding.UTF8.GetString(data);
            })];
    }

    private static void AddCertificates(List<string> certificates)
    {
        if (certificates.Count == 0) return;

        var certificateCollection = new X509Certificate2Collection();

        foreach (var pem in certificates)
        {
            var cert = LoadPemCertificate(pem);
            certificateCollection.Add(cert);
        }

        var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);

        try
        {
            store.Open(OpenFlags.ReadWrite);
            store.AddRange(certificateCollection);
        }
        catch (Exception ex)
        {
            throw new FileLoadException("Root certificate import failed: " + ex.Message, ex);
        }
        finally
        {
            store.Close();
        }
    }

    private static X509Certificate2 LoadPemCertificate(string pem)
    {
        const string header = "-----BEGIN CERTIFICATE-----";
        const string footer = "-----END CERTIFICATE-----";

        var start = pem.IndexOf(header, StringComparison.Ordinal);
        var end = pem.IndexOf(footer, StringComparison.Ordinal);

        if (start < 0 || end < 0)
            throw new InvalidDataException("Invalid PEM certificate format");

        var base64 = pem[(start + header.Length)..end]
                        .Replace("\r", "")
                        .Replace("\n", "")
                        .Trim();

        var derBytes = Convert.FromBase64String(base64);

        return X509CertificateLoader.LoadCertificate(derBytes);
    }

    private static bool IsBase64String(string str)
    {
        var buffer = new Span<byte>(new byte[str.Length]);
        return Convert.TryFromBase64String(str, buffer, out _);
    }
}
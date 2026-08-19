using Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImports;
using System.Linq.Expressions;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Application.Imports.Utilities;

public static class ExpressionBuilder
{
    public static Expression<Func<FileImport, bool>> CreateFilterExpression(GetFileImportsQuery request)
    {
        var param = Expression.Parameter(typeof(FileImport), "x");
        Expression body = Expression.Constant(true);

        if (request.FileName is not null)
        {
            var prop = Expression.Property(param, nameof(FileImport.FileName));
            var value = Expression.Constant(request.FileName);
            body = Expression.AndAlso(body, Expression.Equal(prop, value));
        }
        if (request.GroupKey is not null)
        {
            var prop = Expression.Property(param, nameof(FileImport.GroupKey));
            var value = Expression.Constant(request.GroupKey);
            body = Expression.AndAlso(body, Expression.Equal(prop, value));
        }
        if (request.FileImportStatus is not null)
        {
            var prop = Expression.Property(param, nameof(FileImport.ImportStatus));
            var value = Expression.Constant(request.FileImportStatus);
            body = Expression.AndAlso(body, Expression.Equal(prop, value));
        }
        if (request.FileProcessingStatus is not null)
        {
            var prop = Expression.Property(param, nameof(FileImport.ProcessingStatus));
            var value = Expression.Constant(request.FileProcessingStatus);
            body = Expression.AndAlso(body, Expression.Equal(prop, value));
        }

        return Expression.Lambda<Func<FileImport, bool>>(body, param);
    }
}
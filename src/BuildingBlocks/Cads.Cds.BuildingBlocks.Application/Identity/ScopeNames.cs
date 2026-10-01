namespace Cads.Cds.BuildingBlocks.Application.Identity;

public static class ScopeNames
{
    // Cognito
    public const string Access = "access";

    // Azure AD
    public const string ReportsRead = "reports.read";
    public const string DbAdminExecute = "db.admin.execute";
    public const string AdminS3Manager = "admin.s3.manager";
    public const string SqsAdminManager = "admin.queue.manager";
}
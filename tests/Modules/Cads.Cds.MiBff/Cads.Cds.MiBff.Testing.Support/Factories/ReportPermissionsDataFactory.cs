using AutoFixture;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Cads.Cds.MiBff.Testing.Support.Data;
using Cads.Cds.MiBff.Testing.Support.SpecimenBuilders;

namespace Cads.Cds.MiBff.Testing.Support.Factories;

public class ReportPermissionsDataFactory
{
    private readonly Fixture _fixture;

    private readonly List<string> _reportKeys = [
        TestReportKeyConstants.AnimalSummaryReportKey,
        TestReportKeyConstants.CohortTracingReportKey,
        TestReportKeyConstants.GbCattleDeathsReportKey,
        TestReportKeyConstants.GbCattleRegistrationsReportKey,
        TestReportKeyConstants.HoldingSummaryReportKey,
        TestReportKeyConstants.JourneyByHaulierReportKey,
        TestReportKeyConstants.MovementsAllHoldingsReportKey,
        TestReportKeyConstants.MovementSummaryHoldingReportKey,
        TestReportKeyConstants.ScrapieFlockSchemeAuditReportKey,
        TestReportKeyConstants.SheepGoatInspectionsReportKey,
        TestReportKeyConstants.UnregisteredHerdsFlocksReportKey,
        TestReportKeyConstants.ZonalMovementsSummaryReportKey
    ];

    private readonly List<string> _userIdentifiers = [
        TestAuthConstants.AzureAdEmail,
        "test-user-1@internal.test"
    ];

    private readonly List<string> _roles = [TestReportPermissionConstants.RoleMiAdmin];
    private readonly List<string> _permissions = [TestReportPermissionConstants.PermissionReportView];

    public ReportPermissionsDataFactory()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new IgnoreNavigationProperties());
    }

    public ReportPermissionsData CreateMockData()
    {
        var reportKeysQueue = new Queue<string>(_reportKeys);
        var externalSubjectsQueue = new Queue<string>(_userIdentifiers);
        var emailsQueue = new Queue<string>(_userIdentifiers);

        var miUsers = _fixture.Build<MiUser>()
            .With(x => x.ExternalSubject, externalSubjectsQueue.Dequeue)
            .With(x => x.Email, emailsQueue.Dequeue)
            .CreateMany(_userIdentifiers.Count).ToList();

        var miRole = _fixture.Build<MiRole>()
            .With(x => x.RoleKey, _roles[0])
            .Create();

        var miPermission = _fixture.Build<MiPermission>()
            .With(x => x.PermissionKey, _permissions[0])
            .Create();

        var miReports = _fixture.Build<MiReport>()
            .With(x => x.ReportKey, reportKeysQueue.Dequeue)
            .With(x => x.IsActive, true)
            .CreateMany(_reportKeys.Count).ToList();

        var userIds = miUsers.Select(x => x.UserId).ToList();
        var userIdsQueue = new Queue<Guid>(userIds);
        var miUserRoles = _fixture.Build<MiUserRole>()
            .With(x => x.RoleId, miRole.RoleId)
            .With(x => x.UserId, userIdsQueue.Dequeue)
            .CreateMany(userIds.Count).ToList();

        var miRoleReportPermissions = miReports
            .Select(r => new MiRoleReportPermission
            {
                RoleId = miRole.RoleId,
                ReportId = r.ReportId,
                PermissionId = miPermission.PermissionId,
                Granted = true
            })
            .ToList();

        _fixture.Customizations.Add(new MiEffectiveReportPermissionBuilder(miReports, miUsers));

        var miEffectiveReportPermissions = _fixture.CreateMany<MiEffectiveReportPermission>(_reportKeys.Count * _userIdentifiers.Count).ToList();

        var miEffectiveReportAllPermissions = miEffectiveReportPermissions.Select(x => new MiEffectiveReportAllPermission
        {
            ReportKey = x.ReportKey,
            ExternalSubject = x.ExternalSubject,
            PermissionKey = miPermission.PermissionKey
        }).ToList();

        return new ReportPermissionsData(miUsers,
            [miRole],
            [miPermission],
            miReports,
            miUserRoles,
            miRoleReportPermissions,
            miEffectiveReportPermissions,
            miEffectiveReportAllPermissions);
    }
}
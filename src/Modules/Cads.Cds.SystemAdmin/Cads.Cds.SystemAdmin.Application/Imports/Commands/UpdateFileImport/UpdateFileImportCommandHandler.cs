using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Application.Imports.Services;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.DTOs;
using Cads.Cds.BuildingBlocks.Core.Exceptions;
using Cads.Cds.SystemAdmin.Application.Imports.Repositories;
using MediatR;

namespace Cads.Cds.SystemAdmin.Application.Imports.Commands.UpdateFileImport;

public class UpdateFileImportCommandHandler(
    ISystemAdminFileImportRepository fileImportRepository,
    IS3ImportJobEnqueuer<CreateS3CsvImportJobDto> s3ImportEnqueueService)
    : ICommandHandler<UpdateFileImportCommand, Unit>
{
    public async Task<Unit> Handle(UpdateFileImportCommand command, CancellationToken cancellationToken)
    {
        var fileImport = await fileImportRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(FileImport), command.Id);

        var currentStatus = fileImport.ImportStatus;

        fileImport.SetTotalRowsToProcess(command.TotalRowsToProcess);
        fileImport.SetRowsFound(command.RowsFound);
        fileImport.SetImportStatus(command.ImportStatus);

        if (command.ImportStatus == FileImportStatus.Split && currentStatus != FileImportStatus.Split)
        {
            var job = new CreateS3CsvImportJobDto
            {
                FileImportId = command.Id
            };

            await s3ImportEnqueueService.EnqueueAsync(job, cancellationToken);
        }

        return Unit.Value;
    }
}
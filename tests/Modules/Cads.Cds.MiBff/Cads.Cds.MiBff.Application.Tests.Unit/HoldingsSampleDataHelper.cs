using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Tests.Unit
{
    internal class HoldingsSampleDataHelper
    {
        public static List<HoldingDTO> GetSampleHoldings() =>
        [
            new() { Id = Guid.NewGuid(), Name = "Holding 1", Cph = "ABC123" },
            new() { Id = Guid.NewGuid(), Name = "Holding 2", Cph = "DEF123" },
            new() { Id = Guid.NewGuid(), Name = "Holding 3", Cph = "GHI123" },
            new() { Id = Guid.NewGuid(), Name = "Holding 4", Cph = "JKL123" },
            new() { Id = Guid.NewGuid(), Name = "Holding 5", Cph = "MNO123" },
        ];
    }
}
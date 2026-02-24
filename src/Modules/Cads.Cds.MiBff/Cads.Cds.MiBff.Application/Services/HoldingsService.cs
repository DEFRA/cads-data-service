using Cads.Cds.MiBff.Application.Configuration;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Helpers;
using Microsoft.Extensions.Options;

namespace Cads.Cds.MiBff.Application.Services
{
    public class HoldingsService(IOptions<MiBffConfig> options) : IHoldingsService
    {
        private const string StaticDataFileName = "ukv_holdings.json";

        public async Task<IEnumerable<HoldingDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if (options.Value.StaticData.Enabled)
            {
                var holdings = await LoadHoldingsFromFileAsync(cancellationToken); 
                
                return holdings;
            }

            return [];
        }

        public async Task<IEnumerable<HoldingDTO>> GetByCphAsync(string cph, CancellationToken cancellationToken = default)
        {
            if (options.Value.StaticData.Enabled)
            {
                var holdings = await LoadHoldingsFromFileAsync(cancellationToken);

                return holdings.Where(h => h.Cph.Equals(cph, StringComparison.OrdinalIgnoreCase));
            }

            return [];
        }

        private async Task<IEnumerable<HoldingDTO>> LoadHoldingsFromFileAsync(CancellationToken cancellationToken = default)
        {
            var filePath = Path.Combine(options.Value.StaticData.Path, StaticDataFileName);

            return await FileHelper.ReadJsonFileAsync<List<HoldingDTO>>(filePath, cancellationToken);
        }
    }
}

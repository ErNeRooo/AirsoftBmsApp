using AirsoftBmsApp.Model.Dto.Zone;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.ZoneRestService;

public interface IZoneRestService
{
    public Task<(HttpResult result, ZoneDto Zone)> PostAsync(PostZoneDto postZoneDto);
    public Task<(HttpResult result, ZoneDto Zone)> PutAsync(PutZoneDto putZoneDto, int zoneId);
    public Task<HttpResult> DeleteAsync(int zoneId);
}

using AirsoftBmsApp.Model.Dto.MapPing;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.MapPingRestService;

public interface IMapPingRestService
{
    public Task<(HttpResult result, MapPingDto MapPing)> PostAsync(PostMapPingDto postMapPingDto);
    public Task<HttpResult> DeleteAsync(int mapPingId);
}

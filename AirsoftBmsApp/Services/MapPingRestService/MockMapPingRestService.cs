using AirsoftBmsApp.Model.Dto.MapPing;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.MapPingRestService;

public class MockMapPingRestService(IPlayerDataService playerDataService, IRoomDataService roomDataService) : IMapPingRestService
{
    public async Task<HttpResult> DeleteAsync(int mapPingId)
    {
        return new Success();
    }

    public async Task<(HttpResult result, MapPingDto MapPing)> PostAsync(PostMapPingDto postMapPingDto)
    {
        return (new Success(), new MapPingDto()
        {
            PlayerId = playerDataService.Player.Id,
            BattleId = roomDataService.Room.Battle.BattleId,
            Longitude = postMapPingDto.Longitude,
            Latitude = postMapPingDto.Latitude,
            Accuracy = postMapPingDto.Accuracy,
            Bearing = postMapPingDto.Bearing,
            Time = postMapPingDto.Time,
        });
    }
}

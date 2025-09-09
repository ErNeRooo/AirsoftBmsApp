using AirsoftBmsApp.Model.Dto.Location;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.LocationRestService;

public class MockLocationRestService(IPlayerDataService playerDataService, IRoomDataService roomDataService) : ILocationRestService
{
    public async Task<HttpResult> DeleteAsync(int id)
    {
        return new Success();
    }

    public async Task<(HttpResult result, LocationDto Location)> PostAsync(PostLocationDto postLocationDto)
    {
        return (new Success(), new LocationDto()
        {
            PlayerId = playerDataService.Player.Id,
            BattleId = roomDataService.Room.Battle.BattleId,
            Longitude = postLocationDto.Longitude,
            Latitude = postLocationDto.Latitude,
            Accuracy = postLocationDto.Accuracy,
            Bearing = postLocationDto.Bearing,
            Time = postLocationDto.Time,
            Type = postLocationDto.Type
        });
    }
}

using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.KillRestService;

public class MockKillRestService(IPlayerDataService playerDataService, IRoomDataService roomDataService) : IKillRestService
{
    public async Task<(HttpResult result, KillDto kill)> PostAsync(PostKillDto postKillDto)
    {
        return (new Success(), new KillDto()
        {
            PlayerId = playerDataService.Player.Id,
            BattleId = roomDataService.Room.Battle.BattleId,
            Longitude = postKillDto.Longitude,
            Latitude = postKillDto.Latitude,
            Accuracy = postKillDto.Accuracy,
            Bearing = postKillDto.Bearing,
            Time = postKillDto.Time,
        });
    }
}

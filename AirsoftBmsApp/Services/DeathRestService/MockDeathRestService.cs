using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.DeathRestService;

public class MockDeathRestService(IPlayerDataService playerDataService, IRoomDataService roomDataService) : IDeathRestService
{
    public async Task<(HttpResult result, DeathDto Death)> PostAsync(PostDeathDto postDeathDto)
    {
        return (new Success(), new DeathDto()
        {
            PlayerId = playerDataService.Player.Id,
            BattleId = roomDataService.Room.Battle.BattleId,
            Longitude = postDeathDto.Longitude,
            Latitude = postDeathDto.Latitude,
            Accuracy = postDeathDto.Accuracy,
            Bearing = postDeathDto.Bearing,
            Time = postDeathDto.Time,
        });
    }
}

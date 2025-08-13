using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.BattleRestService;

public class MockBattleRestService(IPlayerDataService playerDataService, IRoomDataService roomDataService) : IBattleRestService
{
    public async Task<(HttpResult result, BattleDto? battle)> PostAsync(PostBattleDto battleDto)
    {
        if (battleDto.Name == "400")
        {
            return (new Failure("Bad Request"), null);
        }
        else if (battleDto.Name == "500")
        {
            return (new Failure("Internal Server Error"), null);
        }
        else
        {
            await Task.Delay(200);

            var battle = new BattleDto
            {
                Name = battleDto.Name,
                BattleId = 1,
                IsActive = false,
                RoomId = roomDataService.Room.Id
            };

            return (new Success(), battle);
        }
    }

    public async Task<(HttpResult result, BattleDto battle)> PutAsync(PutBattleDto battleDto, int battleId)
    {
        if(battleDto.Name == "400")
        {
            return (new Failure("Bad Request"), null);
        }

        return (new Success(), new BattleDto
        {
            Name = string.IsNullOrEmpty(battleDto.Name) ? roomDataService.Room.Battle.Name : battleDto.Name,
            BattleId = battleId,
            IsActive = battleDto.IsActive ?? roomDataService.Room.Battle.IsActive,
            RoomId = roomDataService.Room.Id
        });
    }

    public async Task<HttpResult> DeleteAsync(int battleId)
    {
        return new Success();
    }
}

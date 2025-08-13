using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.BattleRestService;

public interface IBattleRestService
{
    public Task<(HttpResult result, BattleDto battle)> PostAsync(PostBattleDto battleDto);
}

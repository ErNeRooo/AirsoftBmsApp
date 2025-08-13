using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.BattleRestService;

public interface IBattleRestService
{
    public Task<(HttpResult result, BattleDto battle)> PostAsync(PostBattleDto battleDto);
    public Task<(HttpResult result, BattleDto battle)> PutAsync(PutBattleDto battleDto, int battleId);
    public Task<HttpResult> DeleteAsync(int battleId);
}

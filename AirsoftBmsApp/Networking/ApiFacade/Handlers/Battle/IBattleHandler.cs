using AirsoftBmsApp.Model.Dto.Battle;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Battle;

public interface IBattleHandler
{
    public Task<HttpResult> Create(PostBattleDto postBattleDto);
}

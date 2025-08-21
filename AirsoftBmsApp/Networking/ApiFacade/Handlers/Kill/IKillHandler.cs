using AirsoftBmsApp.Model.Dto.Kills;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Kill;

public interface IKillHandler
{
    public Task<HttpResult> Create(PostKillDto postKillDto);
}

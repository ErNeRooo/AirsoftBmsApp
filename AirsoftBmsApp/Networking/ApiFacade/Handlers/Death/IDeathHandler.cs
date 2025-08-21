using AirsoftBmsApp.Model.Dto.Death;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Death;

public interface IDeathHandler
{
    public Task<HttpResult> Create(PostDeathDto postDeathDto);
}

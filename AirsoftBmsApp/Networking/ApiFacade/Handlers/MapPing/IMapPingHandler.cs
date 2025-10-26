using AirsoftBmsApp.Model.Dto.MapPing;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.MapPing;

public interface IMapPingHandler
{
    public Task<HttpResult> Create(PostMapPingDto postMapPingDto);
    public Task<HttpResult> Delete(int id);
}

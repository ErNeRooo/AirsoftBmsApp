using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.PlayerRestService.Abstractions
{
    public interface IPlayerRestService
    {
        Task<HttpResult> TryRequest(PlayerRequestIntent playerRequest);
    }
}

using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.PlayerRestService.Abstractions
{
    public interface IPlayerRestService
    {
        Task<HttpResult> RegisterPlayerAsync(PostPlayerDto playerDto);
    }
}

using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.PlayerRestService.Abstractions
{
    public interface IPlayerRestService
    {
        Task<HttpResult> GetAsync(int playerId);
        Task<HttpResult> PutAsync(PutPlayerDto playerDto, int playerId);
        Task<HttpResult> RegisterAsync(PostPlayerDto playerDto);
        Task<HttpResult> DeleteAsync(int playerId);
    }
}

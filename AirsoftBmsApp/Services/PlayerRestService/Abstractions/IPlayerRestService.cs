using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Login;
using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Model.Dto.Register;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.PlayerRestService.Abstractions
{
    public interface IPlayerRestService
    {
        Task<HttpResult<Player>> RegisterPlayerAsync(PostPlayerDto playerDto);
        Task<HttpResult<Player>> LogInToAccountAsync(LoginAccountDto accountDto);
        Task<HttpResult<Player>> SignUpAccountAsync(RegisterAccountDto accountDto);
    }
}

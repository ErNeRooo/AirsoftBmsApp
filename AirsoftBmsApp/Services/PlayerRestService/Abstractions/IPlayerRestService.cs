using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.PlayerRestService.Abstractions
{
    public interface IPlayerRestService
    {
        public Task<(HttpResult result, PlayerDto? player)> GetMeAsync();
        public Task<(HttpResult result, PlayerDto? player)> GetByIdAsync(int playerId);
        public Task<(HttpResult result, int? playerId)> RegisterAsync(PostPlayerDto playerDto);
        public Task<(HttpResult result, PlayerDto? player)> KickByIdAsync(int playerId);
        public Task<(HttpResult result, PlayerDto? player)> PutAsync(PutPlayerDto playerDto, int playerId);
        public Task<HttpResult> DeleteAsync();
    }
}

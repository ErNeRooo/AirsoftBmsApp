using AirsoftBmsApp.Model.Dto.Player;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Player
{
    public interface IPlayerHandler
    {
        public Task<HttpResult> Register(PostPlayerDto postPlayerDto);
        public Task<HttpResult> LogOut();
        public Task<HttpResult> Update(PutPlayerDto putPlayerDto, int playerId);
        public Task<HttpResult> KickFromRoom(int playerId);
        public Task<HttpResult> KickFromTeam(int playerId);
    }
}

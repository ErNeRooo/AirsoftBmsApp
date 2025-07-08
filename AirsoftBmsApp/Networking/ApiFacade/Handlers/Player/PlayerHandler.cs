using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Player
{
    public class PlayerHandler(
        IPlayerRestService playerRestService,
        IPlayerDataService playerDataService
        ) : IPlayerHandler
    {
        public async Task<HttpResult> LogOut()
        {
            HttpResult result = await playerRestService.DeleteAsync();

            playerDataService.Player = new ObservablePlayer();

            if (result is Failure failure && failure.errorMessage == "") return new Failure("Unhandled error");

            return result;
        }

        public async Task<HttpResult> Register(PostPlayerDto postPlayerDto)
        {
            try
            {
                (HttpResult result, int? playerId) = await playerRestService.RegisterAsync(postPlayerDto);

                if(result is Success) playerDataService.Player = new ObservablePlayer
                {
                    Id = playerId ?? 0,
                    Name = postPlayerDto.Name
                };
                else
                {
                    if (result is Failure failure && failure.errorMessage == "") return new Failure("Unhandled error");

                    return result;
                }

                return result;
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }
    }
}

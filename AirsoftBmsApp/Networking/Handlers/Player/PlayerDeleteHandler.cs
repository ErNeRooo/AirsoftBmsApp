using AirsoftBmsApp.Model;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;

namespace AirsoftBmsApp.Networking.Handlers.Player
{
    public class PlayerDeleteHandler(IPlayerRestService playerRestService, IPlayerDataService playerDataService) : AbstractHandler
    {
        public override async Task<HttpResult> Handle(object request)
        {
            int playerId = playerDataService.Player.Id;

            var result = await playerRestService.TryRequest(new DeletePlayerAsync(playerId));

            switch (result)
            {
                case SuccessBase success:
                    playerDataService.Player = new ObservablePlayer();

                    var nextResult = await base.Handle(request);

                    return nextResult is null ? result : nextResult;
                case Failure failure:
                    return new Failure(failure.errorMessage);
                case Error error:
                    return new Error(error.errorMessage);
                default:
                    throw new InvalidOperationException("Unknown result type");
            }
        }
    }
}

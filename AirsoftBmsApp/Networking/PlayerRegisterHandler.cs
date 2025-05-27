using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;

namespace AirsoftBmsApp.Networking
{
    public class PlayerRegisterHandler(IPlayerRestService playerRestService, IPlayerDataService playerDataService) : AbstractHandler
    {
        public override async Task<HttpResult> Handle(object request)
        {
            dynamic dynamicRequest = request;

            PostPlayerDto postPlayerDto = new PostPlayerDto
            {
                Name = dynamicRequest.Name,
            };

            var result = await playerRestService.RegisterAsync(postPlayerDto);

            switch (result)
            {
                case Success<int> success:
                    playerDataService.Player.Id = success.data;
                    playerDataService.Player.Name = postPlayerDto.Name;

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

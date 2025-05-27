using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.RoomRestService.Abstractions;

namespace AirsoftBmsApp.Networking.Handlers.Room
{
    public class RoomPostHandler(IRoomRestService roomRestService, IRoomDataService roomDataService, IPlayerDataService playerDataService) : AbstractHandler
    {
        public override async Task<HttpResult> Handle(object request)
        {
            dynamic dynamicRequest = request;

            PostRoomDto postRoomDto = new PostRoomDto
            {
                JoinCode = dynamicRequest.JoinCode,
                MaxPlayers = dynamicRequest.MaxPlayers,
                Password = dynamicRequest.Password,
            };

            var result = await roomRestService.TryRequest(new PostRoomAsync(postRoomDto));

            switch (result)
            {
                case Success<int> success:
                    roomDataService.Room.Id = success.data;
                    roomDataService.Room.AdminPlayerId = playerDataService.Player.Id;
                    roomDataService.Room.MaxPlayers = postRoomDto.MaxPlayers;
                    roomDataService.Room.JoinCode = postRoomDto.JoinCode;

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

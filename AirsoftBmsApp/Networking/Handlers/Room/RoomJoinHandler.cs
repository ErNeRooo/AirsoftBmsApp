using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.RoomRestService.Abstractions;

namespace AirsoftBmsApp.Networking.Handlers.Room
{
    public class RoomJoinHandler(IRoomRestService roomRestService, IRoomDataService roomDataService, IPlayerDataService playerDataService) : AbstractHandler
    {
        public override async Task<HttpResult> Handle(object request)
        {
            dynamic dynamicRequest = request;

            JoinRoomDto postRoomDto = new JoinRoomDto
            {
                JoinCode = dynamicRequest.JoinCode,
                Password = dynamicRequest.Password,
            };

            var result = await roomRestService.TryRequest(new JoinRoomAsync(postRoomDto));

            switch (result)
            {
                case Success<int> success:
                    roomDataService.Room.Id = success.data;
                    playerDataService.Player.RoomId = success.data;
                    roomDataService.Room.JoinCode = postRoomDto.JoinCode;
                    roomDataService.Room.Players.Add(playerDataService.Player);

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

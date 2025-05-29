using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.RoomRestService.Abstractions;

namespace AirsoftBmsApp.Networking.Handlers.Room
{
    public class RoomGetByJoinCodeHandler(IRoomRestService roomRestService, IRoomDataService roomDataService) : AbstractHandler
    {
        public override async Task<HttpResult> Handle(object request)
        {
            dynamic dynamicRequest = request;

            string joinCode = dynamicRequest.JoinCode;

            var result = await roomRestService.TryRequest(new GetRoomByJoinCodeAsync(joinCode));

            switch (result)
            {
                case Success<RoomDto> success:
                    roomDataService.Room.Id = success.data.RoomId;
                    roomDataService.Room.JoinCode = success.data.JoinCode;
                    roomDataService.Room.MaxPlayers = success.data.MaxPlayers;
                    roomDataService.Room.AdminPlayerId = success.data.AdminPlayerId;

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

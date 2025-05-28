using AirsoftBmsApp.Model;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.RoomRestService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Networking.Handlers.Room
{
    public class RoomLeaveHandler(IRoomRestService roomRestService, IRoomDataService roomDataService, IPlayerDataService playerDataService) : AbstractHandler
    {
        public override async Task<HttpResult> Handle(object request)
        {
            var result = await roomRestService.TryRequest(new LeaveRoomAsync());

            switch (result)
            {
                case SuccessBase success:
                    roomDataService.Room = new ObservableRoom();
                    playerDataService.Player.RoomId = 0;

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

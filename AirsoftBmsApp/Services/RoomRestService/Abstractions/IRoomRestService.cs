using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.RoomRestService.Abstractions;

namespace AirsoftBmsApp.Services.PlayerRestService.Abstractions
{
    public interface IRoomRestService
    {
        Task<HttpResult> TryRequest(RoomRequestIntent roomRequest);
    }
}

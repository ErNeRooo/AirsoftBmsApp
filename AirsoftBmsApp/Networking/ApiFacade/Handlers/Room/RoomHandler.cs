using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Room;

public class RoomHandler(
    IRoomRestService roomRestService,
    IRoomDataService roomDataService
    ) : IRoomHandler
{
    public async Task<HttpResult> Create(PostRoomDto postRoomDto)
    {
        try
        {
            (HttpResult result, RoomDto? room) = await roomRestService.PostAsync(postRoomDto);

            if (result is Success) roomDataService.Room = new ObservableRoom(room);
            else if (result is Failure failure && failure.errorMessage == "") return new Failure("Unhandled error");

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> Join(JoinRoomDto joinRoomDto)
    {
        try
        {
            (HttpResult result, RoomDto? room) = await roomRestService.JoinAsync(joinRoomDto);

            if (result is Success) roomDataService.Room = new ObservableRoom(room);
            else if (result is Failure failure && failure.errorMessage == "") return new Failure("Unhandled error");

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> Leave()
    {
        try
        {
            HttpResult result = await roomRestService.LeaveAsync();

            if (result is Success) roomDataService.Room = new ObservableRoom();
            else if (result is Failure failure && failure.errorMessage == "") return new Failure("Unhandled error");

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }
}

using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.PlayerRestService.Abstractions;

public interface IRoomRestService
{
    public Task<(HttpResult result, RoomDto? room)> GetByIdAsync(int roomId);
    public Task<(HttpResult result, RoomDto? room)> GetByJoinCodeAsync(string joinCode);
    public Task<(HttpResult result, RoomDto? room)> PutAsync(PutRoomDto roomDto, int roomId);
    public Task<(HttpResult result, RoomDto? room)> PostAsync(PostRoomDto roomDto);
    public Task<HttpResult> DeleteAsync(int roomId);
    public Task<(HttpResult result, RoomDto? room)> JoinAsync(JoinRoomDto roomDto);
    public Task<HttpResult> LeaveAsync();
}   

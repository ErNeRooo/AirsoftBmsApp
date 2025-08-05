using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.PlayerRestService.Abstractions;

public interface IRoomRestService
{
    public Task<(HttpResult result, RoomIncludingRelatedEntitiesDto? room)> GetByIdAsync(int roomId);
    public Task<(HttpResult result, RoomIncludingRelatedEntitiesDto? room)> GetByJoinCodeAsync(string joinCode);
    public Task<(HttpResult result, RoomDto? room)> PutAsync(PutRoomDto roomDto);
    public Task<(HttpResult result, RoomDto? room)> PostAsync(PostRoomDto roomDto);
    public Task<HttpResult> DeleteAsync();
    public Task<(HttpResult result, RoomIncludingRelatedEntitiesDto? room)> JoinAsync(JoinRoomDto roomDto);
    public Task<HttpResult> LeaveAsync();
}   

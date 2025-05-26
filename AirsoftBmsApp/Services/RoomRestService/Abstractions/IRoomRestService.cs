using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.PlayerRestService.Abstractions
{
    public interface IRoomRestService
    {
        Task<HttpResult> GetByIdAsync(int playerId);
        Task<HttpResult> GetByJoinCodeAsync(string joinCode);
        Task<HttpResult> PutAsync(PutRoomDto roomDto, int roomId);
        Task<HttpResult> PostAsync(PostRoomDto roomDto);
        Task<HttpResult> DeleteAsync(int roomId);
        Task<HttpResult> JoinAsync(LogInRoomDto roomDto);
        Task<HttpResult> LeaveAsync();
    }
}

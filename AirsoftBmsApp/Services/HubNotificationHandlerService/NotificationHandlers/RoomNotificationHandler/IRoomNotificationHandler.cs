using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubConnectionService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.RoomNotificationHandler;

public interface IRoomNotificationHandler
{
    void OnRoomUpdated(RoomDto roomDto, ObservableRoom contextRoom);
    void OnRoomJoined(PlayerDto playerDto, ObservableRoom contextRoom);
    Task OnRoomDeleted(IRoomDataService roomDataService, IPlayerDataService playerDataService, IHubConnectionService hubConnectionService);
}

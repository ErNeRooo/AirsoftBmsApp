using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.RoomNotificationHandler;

public interface IRoomNotificationHandler
{
    void OnRoomUpdated(RoomDto roomDto, ObservableRoom contextRoom);
    void OnRoomJoined(PlayerDto playerDto, ObservableRoom contextRoom);
    void OnRoomDeleted(IRoomDataService roomDataService);
}

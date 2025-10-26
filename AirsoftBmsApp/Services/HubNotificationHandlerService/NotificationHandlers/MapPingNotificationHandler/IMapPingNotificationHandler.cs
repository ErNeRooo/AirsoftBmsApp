using AirsoftBmsApp.Model.Dto.MapPing;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.MapPingNotificationHandler;

public interface IMapPingNotificationHandler
{
    void OnMapPingCreated(MapPingDto mapPingDto, ObservableRoom contextRoom);
    void OnMapPingDeleted(int mapPingId, ObservableRoom contextRoom);
}

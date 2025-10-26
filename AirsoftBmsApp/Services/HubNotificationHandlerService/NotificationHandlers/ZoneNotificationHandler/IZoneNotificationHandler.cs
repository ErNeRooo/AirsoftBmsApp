using AirsoftBmsApp.Model.Dto.Zone;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.ZoneNotificationHandler;

public interface IZoneNotificationHandler
{
    void OnZoneCreated(ZoneDto zoneDto, ObservableRoom contextRoom);
    void OnZoneUpdated(ZoneDto zoneDto, ObservableRoom contextRoom);
    void OnZoneDeleted(int zoneId, ObservableRoom contextRoom);
}

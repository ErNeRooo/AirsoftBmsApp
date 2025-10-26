using AirsoftBmsApp.Model.Dto.Location;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.LocationNotificationHandler;

public interface ILocationNotificationHandler
{
    void OnLocationCreated(LocationDto locationDto, ObservableRoom contextRoom);
    void OnLocationDeleted(int locationId, ObservableRoom contextRoom);
}

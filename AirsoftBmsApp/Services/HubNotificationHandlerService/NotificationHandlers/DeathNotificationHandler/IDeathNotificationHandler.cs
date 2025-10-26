using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.DeathNotificationHandler;

public interface IDeathNotificationHandler
{
    void OnDeathCreated(DeathDto deathDto, ObservableRoom contextRoom);
    void OnDeathDeleted(int deathId, ObservableRoom contextRoom);
}

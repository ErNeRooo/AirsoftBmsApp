using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.KillNotificationHandler;

public interface IKillNotificationHandler
{
    void OnKillCreated(KillDto killDto, ObservableRoom contextRoom);
    void OnKillDeleted(int killId, ObservableRoom contextRoom);
}

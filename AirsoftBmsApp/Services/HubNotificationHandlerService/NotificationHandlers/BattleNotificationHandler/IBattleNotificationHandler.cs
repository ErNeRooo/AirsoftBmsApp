using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.GeolocationService;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.BattleNotificationHandler;

public interface IBattleNotificationHandler
{
    void OnBattleCreated(BattleDto battleDto, ObservableRoom contextRoom);
    void OnBattleUpdated(BattleDto battleDto, ObservableRoom contextRoom, IGeolocationService geolocationService);
    void OnBattleDeleted(int battleId, ObservableRoom contextRoom);
}

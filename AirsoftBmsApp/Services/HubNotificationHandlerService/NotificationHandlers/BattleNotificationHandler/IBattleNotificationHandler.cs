using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.BattleNotificationHandler;

public interface IBattleNotificationHandler
{
    void OnBattleCreated(BattleDto battleDto, ObservableRoom contextRoom);
    void OnBattleUpdated(BattleDto battleDto, ObservableRoom contextRoom);
    void OnBattleDeleted(int battleId, ObservableRoom contextRoom);
}

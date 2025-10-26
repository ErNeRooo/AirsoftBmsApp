using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.BattleNotificationHandler;

public class BattleNotificationHandler : IBattleNotificationHandler
{
    public void OnBattleDeleted(int battleId, ObservableRoom contextRoom)
    {
        contextRoom.Battle = null;
    }

    public void OnBattleUpdated(BattleDto battleDto, ObservableRoom contextRoom)
    {
        contextRoom.Battle = new ObservableBattle(battleDto);
    }

    public void OnBattleCreated(BattleDto battleDto, ObservableRoom contextRoom)
    {
        contextRoom.Battle = new ObservableBattle(battleDto);
    }
}

using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.GeolocationService;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.BattleNotificationHandler;

public class BattleNotificationHandler : IBattleNotificationHandler
{
    public void OnBattleDeleted(int battleId, ObservableRoom contextRoom)
    {
        contextRoom.Battle = null;
    }

    public void OnBattleUpdated(BattleDto battleDto, ObservableRoom contextRoom, IGeolocationService geolocationService)
    {
        if (contextRoom.Battle is not null)
        {
            if (contextRoom.Battle.IsActive && !battleDto.IsActive)
            {
                geolocationService.Stop();
            }
            else if (!contextRoom.Battle.IsActive && battleDto.IsActive)
            {
                geolocationService.Start();
            }
        }

        contextRoom.Battle = new ObservableBattle(battleDto);
    }

    public void OnBattleCreated(BattleDto battleDto, ObservableRoom contextRoom)
    {
        contextRoom.Battle = new ObservableBattle(battleDto);
    }
}

using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.PlayerNotificationHandler;

public interface IPlayerNotificationHandler
{
    void OnPlayerUpdated(PlayerDto playerDto, ObservableRoom contextRoom);
    void OnPlayerDeleted(int playerId, ObservableRoom contextRoom);
    void OnPlayerLeftTeam(int playerId, ObservableRoom contextRoom);
    void OnPlayerLeftRoom(int playerId, ObservableRoom contextRoom);
}

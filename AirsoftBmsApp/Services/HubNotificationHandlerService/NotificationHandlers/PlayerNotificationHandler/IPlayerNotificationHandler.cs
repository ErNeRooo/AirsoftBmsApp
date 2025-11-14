using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.GeolocationService;
using AirsoftBmsApp.Services.HubConnectionService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.PlayerNotificationHandler;

public interface IPlayerNotificationHandler
{
    void OnPlayerUpdated(PlayerDto playerDto, ObservableRoom contextRoom);
    void OnPlayerLeftTeam(int playerId, ObservableRoom contextRoom);
    Task OnPlayerLeftRoom(int playerId, IRoomDataService roomDataService, IPlayerDataService playerDataService, IHubConnectionService hubConnectionService, IGeolocationService geolocationService);
}

using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.BattleNotificationHandler;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.DeathNotificationHandler;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.KillNotificationHandler;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.LocationNotificationHandler;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.MapPingNotificationHandler;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.OrderNotificationHandler;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.PlayerNotificationHandler;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.RoomNotificationHandler;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.TeamNotificationHandler;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.ZoneNotificationHandler;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService;

public interface IHubNotificationHandlerService
{
    public IRoomNotificationHandler Room { get; }
    public IPlayerNotificationHandler Player { get; }
    public ITeamNotificationHandler Team { get; }
    public IBattleNotificationHandler Battle { get; }
    public IKillNotificationHandler Kill { get; }
    public IDeathNotificationHandler Death { get; }
    public ILocationNotificationHandler Location { get; }
    public IOrderNotificationHandler Order { get; }
    public IZoneNotificationHandler Zone { get; }
    public IMapPingNotificationHandler MapPing { get; }
}

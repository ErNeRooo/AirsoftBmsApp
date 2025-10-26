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

public class HubNotificationHandlerService(
    IRoomNotificationHandler roomNotificationHandler,
    IPlayerNotificationHandler playerNotificationHandler,
    ITeamNotificationHandler teamNotificationHandler,
    IBattleNotificationHandler battleNotificationHandler,
    IKillNotificationHandler killNotificationHandler,
    IDeathNotificationHandler deathNotificationHandler,
    ILocationNotificationHandler locationNotificationHandler,
    IOrderNotificationHandler orderNotificationHandler,
    IZoneNotificationHandler zoneNotificationHandler,
    IMapPingNotificationHandler mapPingNotificationHandler
    ) : IHubNotificationHandlerService
{
    public IRoomNotificationHandler Room { get; } = roomNotificationHandler;
    public IPlayerNotificationHandler Player { get; } = playerNotificationHandler;
    public ITeamNotificationHandler Team { get; } = teamNotificationHandler;
    public IBattleNotificationHandler Battle { get; } = battleNotificationHandler;
    public IKillNotificationHandler Kill { get; } = killNotificationHandler;
    public IDeathNotificationHandler Death { get; } = deathNotificationHandler;
    public ILocationNotificationHandler Location { get; } = locationNotificationHandler;
    public IOrderNotificationHandler Order { get; } = orderNotificationHandler;
    public IZoneNotificationHandler Zone { get; } = zoneNotificationHandler;
    public IMapPingNotificationHandler MapPing { get; } = mapPingNotificationHandler;
}

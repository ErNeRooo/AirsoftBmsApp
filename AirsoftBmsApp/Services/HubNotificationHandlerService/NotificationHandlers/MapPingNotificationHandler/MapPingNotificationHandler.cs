using AirsoftBmsApp.Model.Dto.MapPing;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.MapPingNotificationHandler;

public class MapPingNotificationHandler : IMapPingNotificationHandler
{
    public void OnMapPingCreated(MapPingDto mapPingDto, ObservableRoom contextRoom, Action refreshMap)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == mapPingDto.PlayerId);
        ObservableTeam? team = contextRoom.Teams.FirstOrDefault(t => t.Id == player?.TeamId);

        if(team is null || team.MapPings.Any(mp => mp.MapPingId == mapPingDto.MapPingId)) return;

        team.MapPings.Add(new ObservableMapPing(mapPingDto));

        refreshMap();
    }

    public void OnMapPingDeleted(int mapPingId, ObservableRoom contextRoom, Action refreshMap)
    {
        List<ObservableMapPing> mapPings = contextRoom.Teams.SelectMany(t => t.MapPings).ToList();
        ObservableMapPing? mapPing = mapPings.FirstOrDefault(mp => mp.MapPingId == mapPingId);

        if (mapPing is null) return;

        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == mapPing.PlayerId);
        ObservableTeam? team = contextRoom.Teams.FirstOrDefault(t => t.Id == player?.TeamId);

        team?.MapPings.Remove(mapPing);

        refreshMap();
    }
}

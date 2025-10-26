using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.KillNotificationHandler;

public class KillNotificationHandler : IKillNotificationHandler
{
    public void OnKillCreated(KillDto killDto, ObservableRoom contextRoom)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == killDto.PlayerId);

        if (player is null || player.Kills.Any(d => d.KillId == killDto.KillId)) return;

        player.Kills.Add(new ObservableKill(killDto));
    }

    public void OnKillDeleted(int killId, ObservableRoom contextRoom)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservableKill? kill = players
            .SelectMany(p => p.Kills)
            .FirstOrDefault(d => d.KillId == killId);

        if (kill is null) return;

        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == kill.PlayerId);

        player?.Kills.Remove(kill);
    }
}

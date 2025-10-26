using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Model.Observable;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.DeathNotificationHandler;

public class DeathNotificationHandler : IDeathNotificationHandler
{
    public void OnDeathCreated(DeathDto deathDto, ObservableRoom contextRoom)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == deathDto.PlayerId);

        if(player is null || player.Deaths.Any(d => d.DeathId == deathDto.DeathId)) return;

        player.Deaths.Add(new ObservableDeath(deathDto));
    }

    public void OnDeathDeleted(int deathId, ObservableRoom contextRoom)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservableDeath? death = players
            .SelectMany(p => p.Deaths)
            .FirstOrDefault(d => d.DeathId == deathId);

        if (death is null) return;

        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == death.PlayerId);

        player?.Deaths.Remove(death);
    }
}

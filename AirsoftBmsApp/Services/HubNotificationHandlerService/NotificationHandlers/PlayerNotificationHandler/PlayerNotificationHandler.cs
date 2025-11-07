using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.PlayerNotificationHandler;

public class PlayerNotificationHandler : IPlayerNotificationHandler
{
    public void OnPlayerDeleted(int playerId, ObservableRoom contextRoom)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == playerId);
        ObservableTeam? team = contextRoom.Teams.FirstOrDefault(t => t.Id == (player?.TeamId ?? 0));

        if(player is not null) team?.Players.Remove(player);
    }

    public void OnPlayerLeftRoom(int playerId, ObservableRoom contextRoom)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == playerId);
        ObservableTeam? team = contextRoom.Teams.FirstOrDefault(t => t.Id == (player?.TeamId ?? 0));

        if (player is not null) team?.Players.Remove(player);
    }

    public void OnPlayerLeftTeam(int playerId, ObservableRoom contextRoom)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == playerId);
        ObservableTeam? previousTeam = contextRoom.Teams.FirstOrDefault(t => t.Id == (player?.TeamId ?? 0));
        ObservableTeam underNoFlagTeam = contextRoom.Teams[0];

        if (player is not null && previousTeam is not null && previousTeam.Id != 0) 
        {
            player.TeamId = 0;
            previousTeam?.Players.Remove(player);
            underNoFlagTeam.Players.Add(player);
        }
    }

    public void OnPlayerUpdated(PlayerDto playerDto, ObservableRoom contextRoom)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? oldPlayer = players.FirstOrDefault(p => p.Id == playerDto.PlayerId);

        if (oldPlayer is null) return;

        if (oldPlayer.TeamId != playerDto.TeamId)
        {
            ObservableTeam? previousTeam = contextRoom.Teams.FirstOrDefault(t => t.Id == oldPlayer.TeamId);
            ObservableTeam? newTeam = contextRoom.Teams.FirstOrDefault(t => t.Id == playerDto.TeamId);
            if (previousTeam is not null && newTeam is not null)
            {
                previousTeam.Players.Remove(oldPlayer);
                newTeam.Players.Add(oldPlayer);
            }
        }

        oldPlayer.Name = playerDto.Name;
        oldPlayer.IsDead = playerDto.IsDead;
        oldPlayer.TeamId = playerDto.TeamId;
    }
}

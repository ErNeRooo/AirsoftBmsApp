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
        if (player?.IsAdmin == true) contextRoom.AdminPlayerId = 0;
        if (team is not null && player?.IsOfficer == true) team.OfficerId = 0;
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
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == playerDto.PlayerId);

        if (player is null) return;

        if (player.TeamId != playerDto.TeamId)
        {
            ObservableTeam? previousTeam = contextRoom.Teams.FirstOrDefault(t => t.Id == (player.TeamId ?? 0));
            ObservableTeam? newTeam = contextRoom.Teams.FirstOrDefault(t => t.Id == playerDto.TeamId);

            if (previousTeam is not null && newTeam is not null)
            {
                previousTeam.Players.Remove(player);
                newTeam.Players.Add(player);

                if (player.IsOfficer)
                {
                    player.IsOfficer = false;
                    previousTeam.OfficerId = 0;
                }
            }
        }

        player.Name = playerDto.Name;
        player.IsDead = playerDto.IsDead;
        player.TeamId = playerDto.TeamId;
    }
}

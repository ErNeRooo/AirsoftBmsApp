using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.GeolocationService;
using AirsoftBmsApp.Services.HubConnectionService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.PlayerNotificationHandler;

public class PlayerNotificationHandler : IPlayerNotificationHandler
{
    public async Task OnPlayerLeftRoom(
        int playerId, 
        IRoomDataService roomDataService, 
        IPlayerDataService playerDataService, 
        IHubConnectionService hubConnectionService, 
        IGeolocationService geolocationService
        )
    {
        ObservableRoom contextRoom = roomDataService.Room;
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == playerId);
        ObservableTeam? team = contextRoom.Teams.FirstOrDefault(t => t.Id == (player?.TeamId ?? 0));

        if (player is not null) team?.Players.Remove(player);
        if (player?.IsAdmin == true) contextRoom.AdminPlayerId = 0;
        if (team is not null && player?.IsOfficer == true) team.OfficerId = 0;

        if (playerId == playerDataService.Player.Id)
        {
            ObservablePlayer oldPlayer = playerDataService.Player;
            roomDataService.Room = new();
            playerDataService.Player = new() { Id = oldPlayer.Id, Name = oldPlayer.Name };

            geolocationService.Stop();
            await hubConnectionService.StopConnection();
            await Shell.Current.GoToAsync("../..", animate: false);
        }
    }

    public void OnPlayerLeftTeam(int playerId, ObservableRoom contextRoom)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == playerId);
        ObservableTeam? previousTeam = contextRoom.Teams.FirstOrDefault(t => t.Id == (player?.TeamId ?? 0));
        ObservableTeam underNoFlagTeam = contextRoom.Teams[0];

        if (player is not null && previousTeam is not null && previousTeam.Id != 0) 
        {
            if(player.IsOfficer)
            {
                player.IsOfficer = false;
                previousTeam.OfficerId = 0;
            }

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

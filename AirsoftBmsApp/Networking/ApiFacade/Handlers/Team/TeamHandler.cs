using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.TeamRestService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Room;

public class TeamHandler(
    ITeamRestService teamRestService,
    IRoomDataService roomDataService,
    IPlayerDataService playerDataService) : ITeamHandler
{
    public async Task<HttpResult> Create(PostTeamDto postTeamDto)
    {
        try
        {
            (HttpResult result, TeamDto? team) = await teamRestService.PostAsync(postTeamDto);

            if (result is Success) roomDataService.Room.Teams.Add(new ObservableTeam(team));
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> Delete(int teamId)
    {
        try
        {
            HttpResult result = await teamRestService.DeleteAsync(teamId);

            if (result is Success)
            {
                ObservableTeam teamToRemove = roomDataService.Room.Teams.FirstOrDefault(t => t.Id == teamId);

                foreach (var player in teamToRemove.Players)
                {
                    if (teamToRemove.OfficerId == player.Id) player.IsOfficer = false;
                    player.TeamId = 0;
                    roomDataService.Room.Teams[0].Players.Add(player);
                }

                roomDataService.Room.Teams.Remove(teamToRemove);
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> Leave()
    {
        try
        {
            HttpResult result = await teamRestService.LeaveAsync();

            if (result is Success)
            {
                roomDataService.Room
                    .Teams.FirstOrDefault(t => t.Id == playerDataService.Player.TeamId)
                    ?.Players.Remove(playerDataService.Player);

                ObservableTeam? previousTeam = roomDataService.Room.Teams
                    .FirstOrDefault(t => t.Id == (playerDataService.Player.TeamId ?? 0));

                playerDataService.Player.TeamId = 0;

                if (previousTeam.OfficerId == playerDataService.Player.Id)
                {
                    playerDataService.Player.IsOfficer = false;
                    previousTeam.OfficerId = 0;
                }

                bool isPlayerInUnderNoFlagTeam = roomDataService.Room.Teams[0].Players.Any(p => p.Id == playerDataService.Player.Id);

                if (!isPlayerInUnderNoFlagTeam)
                    roomDataService.Room.Teams[0].Players.Add(playerDataService.Player);
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> Update(PutTeamDto putTeamDto, int teamId)
    {
        try
        {
            (HttpResult result, TeamDto? team) = await teamRestService.PutAsync(putTeamDto, teamId);

            if (result is Success && team is not null)
            {
                ObservableTeam? teamToUpdate = roomDataService.Room.Teams.FirstOrDefault(t => t.Id == teamId);
                ObservableTeam updatedTeam = new(team);

                if (teamToUpdate is not null)
                {
                    teamToUpdate.Name = team.Name;
                    teamToUpdate.OfficerId = team.OfficerPlayerId;
                    teamToUpdate.SpawnZone = updatedTeam.SpawnZone;
                }
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }
}

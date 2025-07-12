using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Observable;
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
            else if (result is Failure failure && failure.errorMessage == "") return new Failure("Unhandled error");

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

                playerDataService.Player.TeamId = 0;

                roomDataService.Room.Teams[0]
                        .Players.Add(playerDataService.Player);
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure("Unhandled error");

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }
}

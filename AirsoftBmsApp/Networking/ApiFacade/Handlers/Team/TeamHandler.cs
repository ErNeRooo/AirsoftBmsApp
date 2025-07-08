using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.TeamRestService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Room;

public class TeamHandler(
    ITeamRestService teamRestService,
    IRoomDataService roomDataService) : ITeamHandler
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
}

using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.TeamRestService.Abstractions;

namespace AirsoftBmsApp.Services.TeamRestService.Implementations;

public class MockTeamRestService(
    IPlayerDataService playerDataService,
    IRoomDataService roomDataService
    ) : ITeamRestService
{
    public async Task<(HttpResult result, TeamDto? team)> GetByIdAsync(int teamId)
    {
        return (new Success(), new TeamDto
        {
            TeamId = teamId,
            Name = "Mocked Team",
            RoomId = 1,
            OfficerPlayerId = null,
        });
    }

    public async Task<(HttpResult result, TeamDto? team)> PostAsync(PostTeamDto teamDto)
    {
        return (new Success(), new TeamDto
        {
            TeamId = 1,
            Name = teamDto.Name,
            RoomId = 1,
            OfficerPlayerId = 2,
        });
    }

    public async Task<(HttpResult result, TeamDto? team)> PutAsync(PutTeamDto teamDto, int teamId)
    {
        var currentTeam = roomDataService.Room.Teams.FirstOrDefault(t => t.Id == teamId);

        return (new Success(), new TeamDto
        {
            TeamId = teamId,
            Name = string.IsNullOrEmpty(teamDto.Name) ? currentTeam.Name : teamDto.Name,
            RoomId = roomDataService.Room.Id,
            OfficerPlayerId = teamDto.OfficerPlayerId ?? currentTeam.OfficerId,
        });
    }
    public async Task<HttpResult> DeleteAsync(int teamId)
    {
        return new Success();
    }

    public async Task<HttpResult> LeaveAsync()
    {
        return new Success(); 
    }
}

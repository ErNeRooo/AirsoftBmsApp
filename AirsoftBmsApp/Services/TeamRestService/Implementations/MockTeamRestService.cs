using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.TeamRestService.Abstractions;

namespace AirsoftBmsApp.Services.TeamRestService.Implementations;

public class MockTeamRestService : ITeamRestService
{
    public async Task<(HttpResult result, TeamDto? team)> GetByIdAsync(int teamId)
    {
        return (new Success(), new TeamDto
        {
            TeamId = teamId,
            Name = "Mocked Team",
            RoomId = 1,
            OfficerPlayerId = 2,
        });
    }

    public async Task<(HttpResult result, TeamDto? team)> PostAsync(PostTeamDto teamDto)
    {
        return (new Success(), new TeamDto
        {
            TeamId = 1,
            Name = "Mocked Team",
            RoomId = 1,
            OfficerPlayerId = 2,
        });
    }

    public async Task<(HttpResult result, TeamDto? team)> PutAsync(PutTeamDto teamDto, int teamId)
    {
        return (new Success(), new TeamDto
        {
            TeamId = teamId,
            Name = "Mocked Team",
            RoomId = 1,
            OfficerPlayerId = 2,
        });
    }
    public async Task<HttpResult> DeleteAsync(int teamId)
    {
        return new Success();
    }
}

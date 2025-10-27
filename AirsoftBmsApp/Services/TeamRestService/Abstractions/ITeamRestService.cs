using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Dto.Zone;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.TeamRestService.Abstractions;

public interface ITeamRestService
{
    public Task<(HttpResult result, TeamDto? team)> GetByIdAsync(int teamId);
    public Task<(HttpResult result, TeamDto? team)> PutAsync(PutTeamDto teamDto, int teamId);
    public Task<(HttpResult result, TeamDto? team)> PostAsync(PostTeamDto teamDto);
    public Task<(HttpResult result, ZoneDto? team)> PostSpawnAsync(PostZoneDto postZoneDto, int teamId);
    public Task<HttpResult> DeleteAsync(int teamId);
    public Task<HttpResult> LeaveAsync();
}

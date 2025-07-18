﻿using AirsoftBmsApp.Model.Dto.Team;
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
        return (new Success(), new TeamDto
        {
            TeamId = teamId,
            Name = teamDto.Name,
            RoomId = 1,
            OfficerPlayerId = teamDto.OfficerPlayerId,
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

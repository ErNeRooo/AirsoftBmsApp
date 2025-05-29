using AirsoftBmsApp.Model.Dto.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.TeamRestService.Abstractions
{
    public abstract record TeamRequestIntent;

    public record GetTeamByIdAsync(int teamId) : TeamRequestIntent;
    public record PutTeam(int teamId, PutTeamDto teamDto) : TeamRequestIntent;
    public record PostTeam(PostTeamDto teamDto) : TeamRequestIntent;
    public record DeleteTeam(int teamId) : TeamRequestIntent;
}

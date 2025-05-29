using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.TeamRestService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.TeamRestService.Implementations
{
    public class MockTeamRestService : ITeamRestService
    {
        public async Task<HttpResult> TryRequest(TeamRequestIntent roomRequest)
        {
            await Task.Delay(50);

            switch (roomRequest)
            {
                case GetTeamByIdAsync getById:
                    if (getById.teamId == -1)
                        return new Failure("Mocked failure for teamId -1");

                    if (getById.teamId == -2)
                        return new Error("Mocked error for teamId -2");

                    var team = new TeamDto
                    {
                        TeamId = getById.teamId,
                        Name = $"Mock Team {getById.teamId}",
 
                    };

                    return new Success<TeamDto>(team);

                case PutTeam put:
                    if (put.teamId == -1 || put.teamDto?.Name == "fail")
                        return new Failure("Mocked failure for PutTeam");

                    if (put.teamId == -2 || put.teamDto?.Name == "error")
                        return new Error("Mocked error for PutTeam");

                    return new Success<object>(null);

                case PostTeam post:
                    if (post.teamDto?.Name == "fail")
                        return new Failure("Mocked failure for PostTeam");

                    if (post.teamDto?.Name == "error")
                        return new Error("Mocked error for PostTeam");

                    return new Success<object>(null);

                case DeleteTeam delete:
                    if (delete.teamId == -1)
                        return new Failure("Mocked failure for DeleteTeam");

                    if (delete.teamId == -2)
                        return new Error("Mocked error for DeleteTeam");

                    return new Success<object>(null);

                default:
                    return new Failure("Unknown request intent");
            }
        }
    }
}

using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.TeamRestService.Abstractions
{
    public interface ITeamRestService
    {
        Task<HttpResult> TryRequest(TeamRequestIntent roomRequest);
    }
}

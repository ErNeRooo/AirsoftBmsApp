using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.KillRestService;

public interface IKillRestService
{
    public Task<(HttpResult result, KillDto kill)> PostAsync(PostKillDto postKillDto);
}

using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.DeathRestService;

public interface IDeathRestService
{
    public Task<(HttpResult result, DeathDto Death)> PostAsync(PostDeathDto postDeathDto);
}

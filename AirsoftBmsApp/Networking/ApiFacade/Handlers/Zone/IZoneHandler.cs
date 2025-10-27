using AirsoftBmsApp.Model.Dto.Zone;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Zone;

public interface IZoneHandler
{
    public Task<HttpResult> Create(PostZoneDto postZoneDto);
}

using AirsoftBmsApp.Model.Dto.Location;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Location;

public interface ILocationHandler
{
    public Task<HttpResult> Create(PostLocationDto postLocationDto);
}

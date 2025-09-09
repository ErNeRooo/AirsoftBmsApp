using AirsoftBmsApp.Model.Dto.Location;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Location;

public interface ILocationHandler
{
    public Task<HttpResult> Create(PostLocationDto postLocationDto);
    public Task<HttpResult> Delete(int id);
}

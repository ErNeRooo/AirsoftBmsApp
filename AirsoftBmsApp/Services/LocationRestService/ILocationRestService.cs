using AirsoftBmsApp.Model.Dto.Location;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.LocationRestService;

public interface ILocationRestService
{
    public Task<(HttpResult result, LocationDto Location)> PostAsync(PostLocationDto postLocationDto);
    public Task<HttpResult> DeleteAsync(int id);
}

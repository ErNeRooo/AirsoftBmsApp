using AirsoftBmsApp.Model.Dto.Location;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Location;

namespace AirsoftBmsApp.Services.GeolocationService;

public class MockGeolocationService(ILocationHandler locationApiHandler) : IGeolocationService
{
    public async Task Start() { }

    public void Stop() { }
}

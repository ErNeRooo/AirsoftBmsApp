using AirsoftBmsApp.Model.Dto.Location;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Location;

namespace AirsoftBmsApp.Services.GeolocationService;

public class GeolocationService(ILocationHandler locationApiHandler) : IGeolocationService
{
    public async Task Start()
    {
        Geolocation.LocationChanged += OnLocationChanged;
        var request = new GeolocationListeningRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(1));
        if(request != null)
        {
            await Geolocation.StartListeningForegroundAsync(request);
        }
    }

    public void Stop()
    {
        Geolocation.LocationChanged -= OnLocationChanged;
        Geolocation.StopListeningForeground();
    }

    public void OnLocationChanged(object? sender, GeolocationLocationChangedEventArgs e)
    {
        var postLocationDto = new PostLocationDto
        {
            Longitude = e.Location.Longitude,
            Latitude = e.Location.Latitude,
            Accuracy = e.Location.Accuracy ?? 0,
            Bearing = e.Location.Course is null ? 0 : (int)e.Location.Course,
            Time = e.Location.Timestamp,
        };

        locationApiHandler.Create(postLocationDto);
    }
}

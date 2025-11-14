using AirsoftBmsApp.Model.Dto.Location;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Location;

namespace AirsoftBmsApp.Services.GeolocationService;

public class GeolocationService(ILocationHandler locationApiHandler) : IGeolocationService
{
    public async Task Start()
    {
        Geolocation.LocationChanged -= OnLocationChanged;
        Geolocation.LocationChanged += OnLocationChanged;
        var request = new GeolocationListeningRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5));
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
            Bearing = e.Location.Course ?? 0,
            Time = e.Location.Timestamp,
        };

        locationApiHandler.Create(postLocationDto);
    }
}

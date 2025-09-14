namespace AirsoftBmsApp.Services.GeolocationService;

public interface IGeolocationService
{
    Task Start();
    void Stop();
}

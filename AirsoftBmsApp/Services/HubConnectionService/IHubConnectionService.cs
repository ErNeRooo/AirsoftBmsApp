using Microsoft.AspNetCore.SignalR.Client;

namespace AirsoftBmsApp.Services.HubConnectionService;

public interface IHubConnectionService
{
    public HubConnection HubConnection { get; }
    Task StartConnection();
    Task StopConnection();
}

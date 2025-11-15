using AirsoftBmsApp.Services.JwtTokenService;
using Microsoft.AspNetCore.SignalR.Client;

namespace AirsoftBmsApp.Services.HubConnectionService;

public class MockHubConnectionService : IHubConnectionService
{
    public HubConnection HubConnection { get; }

    public MockHubConnectionService()
    {
        HubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost/fakehub") // no server needed for On(...) registrations
            .WithAutomaticReconnect()
            .Build();
    }
    public Task StartConnection()
    {
        return Task.CompletedTask;
    }

    public Task StopConnection()
    {
        return Task.CompletedTask;
    }
}

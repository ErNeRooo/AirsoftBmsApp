using AirsoftBmsApp.Services.JwtTokenService;
using Microsoft.AspNetCore.SignalR.Client;

namespace AirsoftBmsApp.Services.HubConnectionService;

public class HubConnectionService : IHubConnectionService
{
    public HubConnection HubConnection { get; private set; }

    public HubConnectionService(IJwtTokenService jwtTokenService)
    {
        HubConnection = new HubConnectionBuilder()
            .WithUrl("http://10.0.2.2:8080/roomHub", options =>
            {
                options.AccessTokenProvider = async () => 
                {
                    return jwtTokenService.Token;
                };
            }) 
            .WithAutomaticReconnect()
            .Build();
    }

    public async Task StartConnection()
    {
        await HubConnection.StartAsync();
    }

    public async Task StopConnection()
    {
        await HubConnection.StopAsync();
    }
}

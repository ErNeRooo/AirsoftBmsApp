using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubConnectionService;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.RoomNotificationHandler;
using AirsoftBmsApp.Services.PlayerDataService.Implementations;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Implementations;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Shouldly;
using Xunit.Sdk;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.RoomNotificationHandlerTests;

public class RoomNotificationHandler_OnRoomDeleted_Tests
{
    private readonly RoomNotificationHandler _roomNotificationHandler = new RoomNotificationHandler();

    [Fact]
    public void OnRoomDeleted_RoomIsNotNull_ShouldResetRoom()
    {
        // Arrange
        RoomDataService roomDataService = new()
        {
            Room = new()
            {
                Id = 1,
            }
        };
        PlayerDataService playerDataService = new()
        {
            Player = new()
            {
                Id = 1,
                RoomId = 1,
            }
        };
        IHubConnectionService hubConnectionService = new MockConnectionService();

        // Act
        _roomNotificationHandler.OnRoomDeleted(roomDataService, playerDataService, hubConnectionService);

        // Assert
        roomDataService.Room.JoinCode.ShouldBeNull();
        roomDataService.Room.Id.ShouldBe(0);
        roomDataService.Room.MaxPlayers.ShouldBe(0);
        roomDataService.Room.AdminPlayerId.ShouldBeNull();
        roomDataService.Room.Battle?.Zones.Count.ShouldBe(0);
        roomDataService.Room.Teams.Count.ShouldBe(1);
        playerDataService.Player.RoomId.ShouldBeNull();
    }

    private class MockConnectionService : IHubConnectionService
    {
        public HubConnection HubConnection { get; }

        public MockConnectionService()
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
}

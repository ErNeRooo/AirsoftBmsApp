using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubConnectionService;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.PlayerNotificationHandler;
using AirsoftBmsApp.Services.PlayerDataService.Implementations;
using AirsoftBmsApp.Services.RoomDataService.Implementations;
using Microsoft.AspNetCore.SignalR.Client;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.PlayerNotificationHandlerTests;

public class PlayerNotificationHandler_OnPlayerLeftRoom_Tests
{
    private readonly PlayerNotificationHandler _playerNotificationHandler = new PlayerNotificationHandler();

    [Theory]
    [InlineData(1, 1)]
    [InlineData(5, 1)]
    public void OnPlayerLeftRoom_WhenKickedPlayerIsYou_ShouldRemovePlayerAndClearRoom(int targetPlayerId, int expectedPlayersCount)
    {
        // Arrange
        RoomDataService roomDataService = new()
        {
            Room = new ObservableRoom()
            {
                Id = 1,
                Teams = new()
                {
                    new ObservableTeam { Id = 0 },
                    new ObservableTeam
                    {
                        Id = 1,
                        Players = new()
                        {
                            new ObservablePlayer
                            {
                                Id = 1,
                                TeamId = 1
                            },
                            new ObservablePlayer
                            {
                                Id = 5,
                                TeamId = 1
                            }
                        }
                    }
                }
            }
        };
        PlayerDataService playerDataService = new()
        {
            Player = new ObservablePlayer
            {
                Id = targetPlayerId,
                TeamId = 1,
                RoomId = 1
            }
        };
        IHubConnectionService hubConnectionService = new MockHubConnectionService();

        // Act
        _playerNotificationHandler.OnPlayerLeftRoom(targetPlayerId, roomDataService, playerDataService, hubConnectionService);

        // Assert
        ObservablePlayer? player = roomDataService.Room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == targetPlayerId);

        player.ShouldBeNull();
        playerDataService.Player.RoomId.ShouldBeNull();
        roomDataService.Room.JoinCode.ShouldBeNull();
        roomDataService.Room.Id.ShouldBe(0);
        roomDataService.Room.MaxPlayers.ShouldBe(0);
        roomDataService.Room.AdminPlayerId.ShouldBeNull();
        roomDataService.Room.Zones.Count.ShouldBe(0);
        roomDataService.Room.Teams.Count.ShouldBe(1);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(5, 1)]
    public void OnPlayerLeftRoom_WhenThePlayerExists_ShouldRemovePlayer(int targetPlayerId, int expectedPlayersCount)
    {
        // Arrange
        RoomDataService roomDataService = new()
        {
            Room = new ObservableRoom()
            {
                Teams = new()
                {
                    new ObservableTeam { Id = 0 },
                    new ObservableTeam
                    {
                        Id = 1,
                        Players = new()
                        {
                            new ObservablePlayer
                            {
                                Id = 1,
                                TeamId = 1
                            },
                            new ObservablePlayer
                            {
                                Id = 5,
                                TeamId = 1
                            }
                        }
                    }
                }
            }
        };
        PlayerDataService playerDataService = new()
        {
            Player = new ObservablePlayer
            {
                Id = 123123,
                TeamId = 1
            }
        };
        IHubConnectionService hubConnectionService = new MockHubConnectionService();

        // Act
        _playerNotificationHandler.OnPlayerLeftRoom(targetPlayerId, roomDataService, playerDataService, hubConnectionService);

        // Assert
        ObservablePlayer? player = roomDataService.Room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == targetPlayerId);

        player.ShouldBeNull();
        roomDataService.Room.Teams[1].Players.Count.ShouldBe(expectedPlayersCount);
    }

    private class MockHubConnectionService : IHubConnectionService
    {
        public HubConnection HubConnection { get; }

        public MockHubConnectionService()
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost/fakehub")
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

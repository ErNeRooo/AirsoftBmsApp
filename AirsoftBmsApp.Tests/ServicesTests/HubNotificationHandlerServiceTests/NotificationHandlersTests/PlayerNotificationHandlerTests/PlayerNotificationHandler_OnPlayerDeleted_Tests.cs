using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.PlayerNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.PlayerNotificationHandlerTests;

public class PlayerNotificationHandler_OnPlayerDeleted_Tests
{
    private readonly PlayerNotificationHandler _playerNotificationHandler = new PlayerNotificationHandler();

    [Theory]
    [InlineData(1, 1)]
    [InlineData(5, 1)]
    public void OnPlayerDeleted_WhenThePlayerExists_ShouldRemovePlayer(int targetPlayerId, int expectedPlayersCount)
    {
        // Arrange
        ObservableRoom room = new()
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
        };

        // Act
        _playerNotificationHandler.OnPlayerDeleted(targetPlayerId, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == targetPlayerId);

        player.ShouldBeNull();
        room.Teams[1].Players.Count.ShouldBe(expectedPlayersCount);
    }
}

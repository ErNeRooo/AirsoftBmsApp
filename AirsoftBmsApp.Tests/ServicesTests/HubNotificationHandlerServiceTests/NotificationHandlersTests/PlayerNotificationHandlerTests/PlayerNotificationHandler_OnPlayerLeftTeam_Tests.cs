using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.PlayerNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.PlayerNotificationHandlerTests;

public class PlayerNotificationHandler_OnPlayerLeftTeam_Tests
{
    private readonly PlayerNotificationHandler _playerNotificationHandler = new PlayerNotificationHandler();

    [Theory]
    [InlineData(1, 1)]
    [InlineData(5, 1)]
    public void OnPlayerLeftTeam_WhenThePlayerExists_ShouldMovePlayerToUnderNoFlagTeam(int targetPlayerId, int expectedPlayersCount)
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
                },
            }
        };

        // Act
        _playerNotificationHandler.OnPlayerLeftTeam(targetPlayerId, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == targetPlayerId);

        player.ShouldNotBeNull();
        player.TeamId.ShouldBe(0);
        room.Teams[0].Players.Count.ShouldBe(1);
        room.Teams[1].Players.Count.ShouldBe(expectedPlayersCount);
    }

    [Theory]
    [InlineData(1, 1)]
    public void OnPlayerLeftTeam_WhenThePlayerIsOfficer_ShouldClearOfficerId(int targetPlayerId, int leftTeamId)
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
                    OfficerId = 1,
                    Players = new()
                    {
                        new ObservablePlayer
                        {
                            Id = 1,
                            TeamId = 1,
                            IsOfficer = true
                        },
                    }
                }
            }
        };

        // Act
        _playerNotificationHandler.OnPlayerLeftTeam(targetPlayerId, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == targetPlayerId);

        ObservableTeam? team = room.Teams
            .FirstOrDefault(t => t.Id == leftTeamId);

        player.ShouldNotBeNull();
        player.TeamId.ShouldBe(0);
        player.IsOfficer.ShouldBeFalse();
        team.OfficerId.ShouldBe(0);
    }
}

using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.PlayerNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.PlayerNotificationHandlerTests;

public class PlayerNotificationHandler_OnPlayerUpdated_Tests
{
    private readonly PlayerNotificationHandler _playerNotificationHandler = new PlayerNotificationHandler();

    [Fact]
    public void OnPlayerUpdated_ShouldUpdatePlayer()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Teams = new()
            {
                new ObservableTeam
                {
                    Id = 1,
                    Players = new()
                    {
                        new ObservablePlayer
                        {
                            Id = 5,
                            TeamId = 1,
                            IsDead = true,
                            Name = "OldName",
                        }
                    }
                },
                new ObservableTeam
                {
                    Id = 2,
                    Players = new()
                }
            }
        };
        PlayerDto playerDto = new()
        {
            PlayerId = 5,
            TeamId = 2,
            IsDead = false,
            Name = "New Name",
        };

        // Act
        _playerNotificationHandler.OnPlayerUpdated(playerDto, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == playerDto.PlayerId);

        room.Teams[0].Players.Count.ShouldBe(0);
        room.Teams[1].Players.Count.ShouldBe(1);
        player.ShouldNotBeNull();
        player.TeamId.ShouldBe(playerDto.TeamId);
        player.IsDead.ShouldBe(playerDto.IsDead);
        player.Name.ShouldBe(playerDto.Name);
    }
}

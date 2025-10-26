using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.DeathNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.DeathNotificationHandlerTests;

public class DeathNotificationHandler_OnDeathDeleted_Tests
{
    private readonly DeathNotificationHandler _deathNotificationHandler = new DeathNotificationHandler();

    [Theory]
    [InlineData(2, 5, 0)]
    [InlineData(3, 1, 1)]
    public void OnDeathDeleted_WhenTheDeathExists_ShouldRemoveDeath(int targetDeathId, int expectedPlayerId, int expectedDeathCount)
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
                            Deaths = new ObservableCollection<ObservableDeath>()
                            {
                                new()
                                {
                                    DeathId = 1,
                                    PlayerId = 1
                                },
                                new()
                                {
                                    DeathId = 3,
                                    PlayerId = 1
                                },
                            }
                        },
                        new ObservablePlayer
                        {
                            Id = 5,
                            Deaths = new ObservableCollection<ObservableDeath>()
                            {
                                new()
                                {
                                    DeathId = 2,
                                    PlayerId = 5
                                },
                            }
                        }
                    }
                }
            }
        };

        // Act
        _deathNotificationHandler.OnDeathDeleted(targetDeathId, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == expectedPlayerId);

        player.Deaths.Count.ShouldBe(expectedDeathCount);
    }
}

using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.KillNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.KillNotificationHandlerTests;

public class KillNotificationHandler_OnKillDeleted_Tests
{
    private readonly KillNotificationHandler _killNotificationHandler = new KillNotificationHandler();

    [Theory]
    [InlineData(2, 5, 0)]
    [InlineData(3, 1, 1)]
    public void OnKillDeleted_WhenTheKillExists_ShouldRemoveKill(int targetKillId, int expectedPlayerId, int expectedKillCount)
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
                            Kills = new ObservableCollection<ObservableKill>()
                            {
                                new()
                                {
                                    KillId = 1,
                                    PlayerId = 1
                                },
                                new()
                                {
                                    KillId = 3,
                                    PlayerId = 1
                                },
                            }
                        },
                        new ObservablePlayer
                        {
                            Id = 5,
                            Kills = new ObservableCollection<ObservableKill>()
                            {
                                new()
                                {
                                    KillId = 2,
                                    PlayerId = 5
                                },
                            }
                        }
                    }
                }
            }
        };

        // Act
        _killNotificationHandler.OnKillDeleted(targetKillId, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == expectedPlayerId);

        player.Kills.Count.ShouldBe(expectedKillCount);
    }
}

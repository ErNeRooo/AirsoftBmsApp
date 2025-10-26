using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.KillNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.KillNotificationHandlerTests;

public class KillNotificationHandler_OnKillCreated_Tests
{
    private readonly KillNotificationHandler _killNotificationHandler = new KillNotificationHandler();

    [Fact]
    public void OnKillCreated_WhenTheKillDoesNotExist_ShouldAddKillToPlayerKills()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Teams = new()
            {
                new ObservableTeam
                {
                    Players = new()
                    {
                        new ObservablePlayer
                        {
                            Id = 1,
                            Kills = new()
                        },
                        new ObservablePlayer
                        {
                            Id = 5,
                            Kills = new()
                        }
                    }
                }
            }
        };
        KillDto killDto = new()
        {
            KillId = 1,
            PlayerId = 5,
            BattleId = 1,
            Longitude = 50.0,
            Latitude = 8.0,
            Accuracy = 2,
            Bearing = 90,
            Time = DateTimeOffset.UtcNow
        };

        // Act
        _killNotificationHandler.OnKillCreated(killDto, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == killDto.PlayerId);

        player.Kills.Count.ShouldBe(1);
    }

    [Fact]
    public void OnKillCreated_WhenTheKillExists_ShouldNotMakeAnyChanges()
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
                            Kills = new()
                        },
                        new ObservablePlayer
                        {
                            Id = 5,
                            Kills = new ObservableCollection<ObservableKill>()
                            {
                                new()
                                {
                                    KillId = 1,
                                    PlayerId = 5,
                                    BattleId = 1,
                                    Longitude = 50.0,
                                    Latitude = 8.0,
                                    Accuracy = 2,
                                    Bearing = 90,
                                    Time = DateTimeOffset.UtcNow
                                }
                            }
                        }
                    }
                }
            }
        };
        KillDto killDto = new()
        {
            KillId = 1,
            PlayerId = 5,
            BattleId = 1,
            Longitude = 50.0,
            Latitude = 8.0,
            Accuracy = 2,
            Bearing = 90,
            Time = DateTimeOffset.UtcNow
        };

        // Act
        _killNotificationHandler.OnKillCreated(killDto, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == killDto.PlayerId);

        player.Kills.Count.ShouldBe(1);
    }
}

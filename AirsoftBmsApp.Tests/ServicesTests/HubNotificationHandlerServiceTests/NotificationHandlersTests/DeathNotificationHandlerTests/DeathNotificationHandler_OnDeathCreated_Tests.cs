using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.DeathNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.DeathNotificationHandlerTests;

public class DeathNotificationHandler_OnDeathCreated_Tests
{
    private readonly DeathNotificationHandler _deathNotificationHandler = new DeathNotificationHandler();

    [Fact]
    public void OnDeathCreated_WhenTheDeathDoesNotExist_ShouldAddDeathToPlayerDeaths()
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
                            Deaths = new()
                        },
                        new ObservablePlayer
                        {
                            Id = 5,
                            Deaths = new()
                        }
                    }
                }
            }
        };
        DeathDto deathDto = new()
        {
            DeathId = 1,
            PlayerId = 5,
            BattleId = 1,
            Longitude = 50.0,
            Latitude = 8.0,
            Accuracy = 2,
            Bearing = 90,
            Time = DateTimeOffset.UtcNow
        };

        // Act
        _deathNotificationHandler.OnDeathCreated(deathDto, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == deathDto.PlayerId);

        player.Deaths.Count.ShouldBe(1);
    }

    [Fact]
    public void OnDeathCreated_WhenTheDeathExists_ShouldNotMakeAnyChanges()
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
                            Deaths = new()
                        },
                        new ObservablePlayer
                        {
                            Id = 5,
                            Deaths = new ObservableCollection<ObservableDeath>()
                            {
                                new()
                                {
                                    DeathId = 1,
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
        DeathDto deathDto = new()
        {
            DeathId = 1,
            PlayerId = 5,
            BattleId = 1,
            Longitude = 50.0,
            Latitude = 8.0,
            Accuracy = 2,
            Bearing = 90,
            Time = DateTimeOffset.UtcNow
        };

        // Act
        _deathNotificationHandler.OnDeathCreated(deathDto, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == deathDto.PlayerId);

        player.Deaths.Count.ShouldBe(1);
    }
}

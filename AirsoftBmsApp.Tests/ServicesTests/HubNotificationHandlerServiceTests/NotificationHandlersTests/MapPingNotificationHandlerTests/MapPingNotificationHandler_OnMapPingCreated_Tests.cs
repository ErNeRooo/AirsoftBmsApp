using AirsoftBmsApp.Model.Dto.MapPing;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.MapPingNotificationHandler;
using Shouldly;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.MapPingNotificationHandlerTests;

public class MapPingNotificationHandler_OnMapPingCreated_Tests
{
    private readonly MapPingNotificationHandler _mapPingNotificationHandler = new MapPingNotificationHandler();

    [Fact]
    public void OnMapPingCreated_WhenTheMapPingDoesNotExist_ShouldAddMapPingToTeamMapPings()
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
                            Id = 1,
                            TeamId = 1

                        },
                        new ObservablePlayer
                        {
                            Id = 5,
                            TeamId = 1
                        }
                    },
                    MapPings = new()
                }
            }
        };
        MapPingDto mapPingDto = new()
        {
            MapPingId = 1,
            PlayerId = 5,
            BattleId = 1,
            Longitude = 50.0,
            Latitude = 8.0,
            Accuracy = 2,
            Bearing = 90,
            Time = DateTimeOffset.UtcNow
        };

        // Act
        _mapPingNotificationHandler.OnMapPingCreated(mapPingDto, room, () => { });

        // Assert
        int? playersTeamId = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == mapPingDto.PlayerId)?.TeamId;

        ObservableTeam? team = room.Teams
            .FirstOrDefault(t => t.Id == playersTeamId);

        team.ShouldNotBeNull();
        team.MapPings.Count.ShouldBe(1);
    }

    [Fact]
    public void OnMapPingCreated_WhenTheMapPingExists_ShouldNotMakeAnyChanges()
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
                    },
                    MapPings = new()
                    {
                        new()
                        {
                            MapPingId = 1,
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
        };
        MapPingDto mapPingDto = new()
        {
            MapPingId = 1,
            PlayerId = 5,
            BattleId = 1,
            Longitude = 50.0,
            Latitude = 8.0,
            Accuracy = 2,
            Bearing = 90,
            Time = DateTimeOffset.UtcNow
        };

        // Act
        _mapPingNotificationHandler.OnMapPingCreated(mapPingDto, room, () => { });

        // Assert
        int? playersTeamId = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == mapPingDto.PlayerId)?.TeamId;

        ObservableTeam? team = room.Teams
            .FirstOrDefault(t => t.Id == playersTeamId);

        team.ShouldNotBeNull();
        team.MapPings.Count.ShouldBe(1);
    }
}

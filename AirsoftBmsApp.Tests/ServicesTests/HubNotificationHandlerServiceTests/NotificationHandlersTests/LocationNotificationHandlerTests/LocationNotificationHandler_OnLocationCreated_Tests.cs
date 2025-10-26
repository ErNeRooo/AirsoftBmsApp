using AirsoftBmsApp.Model.Dto.Location;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.LocationNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.LocationNotificationHandlerTests;

public class LocationNotificationHandler_OnLocationCreated_Tests
{
    private readonly LocationNotificationHandler _locationNotificationHandler = new LocationNotificationHandler();

    [Fact]
    public void OnLocationCreated_WhenTheLocationDoesNotExist_ShouldAddLocationToPlayerLocations()
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
                            Locations = new()
                        },
                        new ObservablePlayer
                        {
                            Id = 5,
                            Locations = new()
                        }
                    }
                }
            }
        };
        LocationDto locationDto = new()
        {
            LocationId = 1,
            PlayerId = 5,
            BattleId = 1,
            Longitude = 50.0,
            Latitude = 8.0,
            Accuracy = 2,
            Bearing = 90,
            Time = DateTimeOffset.UtcNow
        };

        // Act
        _locationNotificationHandler.OnLocationCreated(locationDto, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == locationDto.PlayerId);

        player.Locations.Count.ShouldBe(1);
    }

    [Fact]
    public void OnLocationCreated_WhenTheLocationExists_ShouldNotMakeAnyChanges()
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
                            Locations = new()
                        },
                        new ObservablePlayer
                        {
                            Id = 5,
                            Locations = new ObservableCollection<ObservableLocation>()
                            {
                                new()
                                {
                                    LocationId = 1,
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
        LocationDto locationDto = new()
        {
            LocationId = 1,
            PlayerId = 5,
            BattleId = 1,
            Longitude = 50.0,
            Latitude = 8.0,
            Accuracy = 2,
            Bearing = 90,
            Time = DateTimeOffset.UtcNow
        };

        // Act
        _locationNotificationHandler.OnLocationCreated(locationDto, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == locationDto.PlayerId);

        player.Locations.Count.ShouldBe(1);
    }
}

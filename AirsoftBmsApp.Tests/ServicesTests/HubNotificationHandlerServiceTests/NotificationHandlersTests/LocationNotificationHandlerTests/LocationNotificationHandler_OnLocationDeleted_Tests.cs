using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.LocationNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.LocationNotificationHandlerTests;

public class LocationNotificationHandler_OnLocationDeleted_Tests
{
    private readonly LocationNotificationHandler _locationNotificationHandler = new LocationNotificationHandler();

    [Theory]
    [InlineData(2, 5, 0)]
    [InlineData(3, 1, 1)]
    public void OnLocationDeleted_WhenTheLocationExists_ShouldRemoveLocation(int targetLocationId, int expectedPlayerId, int expectedLocationCount)
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
                            Locations = new ObservableCollection<ObservableLocation>()
                            {
                                new()
                                {
                                    LocationId = 1,
                                    PlayerId = 1
                                },
                                new()
                                {
                                    LocationId = 3,
                                    PlayerId = 1
                                },
                            }
                        },
                        new ObservablePlayer
                        {
                            Id = 5,
                            Locations = new ObservableCollection<ObservableLocation>()
                            {
                                new()
                                {
                                    LocationId = 2,
                                    PlayerId = 5
                                },
                            }
                        }
                    }
                }
            }
        };

        // Act
        _locationNotificationHandler.OnLocationDeleted(targetLocationId, room);

        // Assert
        ObservablePlayer? player = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == expectedPlayerId);

        player.Locations.Count.ShouldBe(expectedLocationCount);
    }
}

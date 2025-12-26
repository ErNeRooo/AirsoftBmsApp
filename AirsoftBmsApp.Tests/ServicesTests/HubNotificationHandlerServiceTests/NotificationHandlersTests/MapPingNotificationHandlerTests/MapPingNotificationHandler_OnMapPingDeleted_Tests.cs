using AirsoftBmsApp.Model.Dto.MapPing;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.MapPingNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.MapPingNotificationHandlerTests;

public class MapPingNotificationHandler_OnMapPingDeleted_Tests
{
    private readonly MapPingNotificationHandler _mapPingNotificationHandler = new MapPingNotificationHandler();

    [Theory]
    [InlineData(1, 1, 0)]
    public void OnMapPingDeleted_WhenTheMapPingExists_ShouldRemoveMapPing(int targetMapPingId, int expectedTeamId, int expectedMapPingCount)
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

        // Act
        _mapPingNotificationHandler.OnMapPingDeleted(targetMapPingId, room, () => { });

        // Assert
        ObservableTeam? team = room.Teams
            .FirstOrDefault(t => t.Id == expectedTeamId);

        team.ShouldNotBeNull();
        team.MapPings.Count.ShouldBe(expectedMapPingCount);
    }
}

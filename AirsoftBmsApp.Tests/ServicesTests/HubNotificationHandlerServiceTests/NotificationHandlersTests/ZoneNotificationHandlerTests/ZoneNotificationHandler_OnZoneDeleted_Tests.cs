using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.ZoneNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.ZoneNotificationHandlerTests;

public class ZoneNotificationHandler_OnZoneDeleted_Tests
{
    private readonly ZoneNotificationHandler _zoneNotificationHandler = new ZoneNotificationHandler();

    [Fact]
    public void OnZoneDeleted_TheZoneExists_ShouldRemoveZone()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
            Battle = new()
            {
                Zones = new ObservableCollection<ObservableZone>()
                {
                    new()
                    {
                        ZoneId = 10,
                        Name = "Test Zone",
                        Type = "Test Type",
                        BattleId = 1,
                        Vertices = new ObservableCollection<ObservableVertex>()
                        {
                            new() { Latitude = 1, Longitude = 1 },
                            new() { Latitude = 2, Longitude = 2 },
                            new() { Latitude = 3, Longitude = 3 },
                        },
                    }
                }
            }
        };
        int targetZoneId = 10;

        // Act
        _zoneNotificationHandler.OnZoneDeleted(targetZoneId, room, () => { });

        // Assert
        ObservableZone? zone = room.Battle.Zones.FirstOrDefault(z => z.ZoneId == targetZoneId);

        room.Battle.Zones.Count.ShouldBe(0);
        zone.ShouldBeNull();
    }
}

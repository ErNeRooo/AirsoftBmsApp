using AirsoftBmsApp.Model.Dto.Zone;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.ZoneNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.ZoneNotificationHandlerTests;

public class ZoneNotificationHandler_OnZoneUpdated_Tests
{
    private readonly ZoneNotificationHandler _zoneNotificationHandler = new ZoneNotificationHandler();

    [Fact]
    public void OnZoneUpdated_TheZoneExists_ShouldUpdateZone()
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
                        Name = "Old Zone",
                        Type = "Old Test Type",
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
        ZoneDto zoneDto = new()
        {
            ZoneId = 10,
            Name = "New Zone",
            Type = "New Test Type",
            BattleId = 2,
        };

        // Act
        _zoneNotificationHandler.OnZoneUpdated(zoneDto, room);

        // Assert
        ObservableZone? zone = room.Battle.Zones.FirstOrDefault(z => z.ZoneId == zoneDto.ZoneId);

        room.Battle.Zones.Count.ShouldBe(1);
        zone.ShouldNotBeNull();
        zone.ZoneId.ShouldBe(zoneDto.ZoneId);
        zone.Name.ShouldBe(zoneDto.Name);
        zone.Type.ShouldBe(zoneDto.Type);
        zone.BattleId.ShouldBe(zoneDto.BattleId);
        zone.Vertices.Count.ShouldBe(3);
    }
}

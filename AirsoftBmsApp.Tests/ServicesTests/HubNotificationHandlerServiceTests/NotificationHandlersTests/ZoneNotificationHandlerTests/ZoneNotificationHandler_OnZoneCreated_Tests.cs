using AirsoftBmsApp.Model.Dto.Vertex;
using AirsoftBmsApp.Model.Dto.Zone;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.ZoneNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.ZoneNotificationHandlerTests;

public class ZoneNotificationHandler_OnZoneCreated_Tests
{
    private readonly ZoneNotificationHandler _zoneNotificationHandler = new ZoneNotificationHandler();

    [Fact]
    public void OnZoneCreated_TheZoneDoesNotExist_ShouldSetAddZone()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
            Battle = new()
            {
                Zones = new ObservableCollection<ObservableZone>()
            }
        };
        ZoneDto zoneDto = new()
        {
            ZoneId = 10,
            Name = "Test Zone",
            Type = "Test Type",
            BattleId = 1,
            Vertices = new List<VertexDto>()
            {
                new() { Latitude = 1, Longitude = 1 },
                new() { Latitude = 2, Longitude = 2 },
                new() { Latitude = 3, Longitude = 3 },
            },
        };

        // Act
        _zoneNotificationHandler.OnZoneCreated(zoneDto, room, () => { });

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

    [Fact]
    public void OnZoneCreated_TheZoneExists_ShouldNotMakeAnyChanges()
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
        ZoneDto zoneDto = new()
        {
            ZoneId = 10,
        };

        // Act
        _zoneNotificationHandler.OnZoneCreated(zoneDto, room, () => { });

        // Assert
        ObservableZone? zone = room.Battle.Zones.FirstOrDefault(z => z.ZoneId == zoneDto.ZoneId);

        room.Battle.Zones.Count.ShouldBe(1);
        zone.ShouldNotBeNull();
        zone.ZoneId.ShouldBe(zoneDto.ZoneId);
        zone.Name.ShouldBe("Test Zone");
        zone.Type.ShouldBe("Test Type");
        zone.BattleId.ShouldBe(1);
        zone.Vertices.Count.ShouldBe(3);
    }
}

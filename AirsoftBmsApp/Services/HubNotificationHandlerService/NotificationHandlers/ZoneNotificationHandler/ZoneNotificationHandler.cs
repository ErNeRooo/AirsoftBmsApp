using AirsoftBmsApp.Model.Dto.Zone;
using AirsoftBmsApp.Model.Observable;
using CommunityToolkit.Maui.Core.Extensions;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.ZoneNotificationHandler;

public class ZoneNotificationHandler : IZoneNotificationHandler
{
    public void OnZoneCreated(ZoneDto zoneDto, ObservableRoom contextRoom)
    {
        bool doesZoneExist = contextRoom.Zones.Any(z => z.ZoneId == zoneDto.ZoneId);

        if (doesZoneExist) return;

        contextRoom.Zones.Add(new ObservableZone(zoneDto));
    }

    public void OnZoneDeleted(int zoneId, ObservableRoom contextRoom)
    {
        ObservableZone? zone = contextRoom.Zones.FirstOrDefault(t => t.ZoneId == zoneId);

        if (zone is null) return;

        contextRoom.Zones.Remove(zone);
    }

    public void OnZoneUpdated(ZoneDto zoneDto, ObservableRoom contextRoom)
    {
        ObservableZone? previousZone = contextRoom.Zones.FirstOrDefault(t => t.ZoneId == zoneDto.ZoneId);

        if (previousZone is null) return;

        previousZone.BattleId = zoneDto.BattleId;
        previousZone.Name = zoneDto.Name;
        previousZone.Type = zoneDto.Type;
    }
}

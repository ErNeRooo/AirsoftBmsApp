using AirsoftBmsApp.Model.Dto.Zone;
using AirsoftBmsApp.Model.Observable;
using CommunityToolkit.Maui.Core.Extensions;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.ZoneNotificationHandler;

public class ZoneNotificationHandler : IZoneNotificationHandler
{
    public void OnZoneCreated(ZoneDto zoneDto, ObservableRoom contextRoom, Action refreshMap)
    {
        if(contextRoom.Battle is null) return;

        bool doesZoneExist = contextRoom.Battle.Zones.Any(z => z.ZoneId == zoneDto.ZoneId);

        if (doesZoneExist) return;

        contextRoom.Battle.Zones.Add(new ObservableZone(zoneDto));

        refreshMap();
    }

    public void OnZoneDeleted(int zoneId, ObservableRoom contextRoom, Action refreshMap)
    {
        if (contextRoom.Battle is null) return;

        ObservableZone? zone = contextRoom.Battle.Zones.FirstOrDefault(t => t.ZoneId == zoneId);

        if (zone is null) return;

        contextRoom.Battle.Zones.Remove(zone);

        refreshMap();
    }

    public void OnZoneUpdated(ZoneDto zoneDto, ObservableRoom contextRoom, Action refreshMap)
    {
        if (contextRoom.Battle is null) return;

        ObservableZone? previousZone = contextRoom.Battle.Zones.FirstOrDefault(t => t.ZoneId == zoneDto.ZoneId);

        if (previousZone is null) return;

        previousZone.BattleId = zoneDto.BattleId;
        previousZone.Name = zoneDto.Name;
        previousZone.Type = zoneDto.Type;

        refreshMap();
    }
}

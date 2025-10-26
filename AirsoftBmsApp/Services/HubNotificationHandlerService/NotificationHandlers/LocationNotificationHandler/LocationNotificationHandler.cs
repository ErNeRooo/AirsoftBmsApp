using AirsoftBmsApp.Model.Dto.Location;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.LocationNotificationHandler;

public class LocationNotificationHandler : ILocationNotificationHandler
{
    public void OnLocationCreated(LocationDto locationDto, ObservableRoom contextRoom)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == locationDto.PlayerId);

        if (player is null || player.Locations.Any(d => d.LocationId == locationDto.LocationId)) return;

        player.Locations.Add(new ObservableLocation(locationDto));
    }

    public void OnLocationDeleted(int locationId, ObservableRoom contextRoom)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservableLocation? location = players
            .SelectMany(p => p.Locations)
            .FirstOrDefault(d => d.LocationId == locationId);

        if (location is null) return;

        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == location.PlayerId);

        player?.Locations.Remove(location);
    }
}

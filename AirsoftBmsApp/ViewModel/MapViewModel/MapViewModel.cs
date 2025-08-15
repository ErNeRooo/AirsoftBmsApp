using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Maps;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.ViewModel.MapViewModel;

public partial class MapViewModel : ObservableObject, IMapViewModel
{
    [ObservableProperty]
    ObservableRoom room;

    [ObservableProperty]
    ObservablePlayer player;

    [ObservableProperty]
    ObservableCollection<Pin> visiblePlayers;

    public MapViewModel(IRoomDataService roomDataService, IPlayerDataService playerDataService)
    {
        Room = roomDataService.Room;
        Player = playerDataService.Player;
        
        Player.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(ObservablePlayer.TeamId))
            {
                UpdateVisiblePlayers();
                UpdatePlayersCollectionChangeHandlers();
            }
        };

        Room.Teams.CollectionChanged += (s, e) =>
        {
            UpdateVisiblePlayers();
            UpdatePlayersCollectionChangeHandlers();
        };
    }

    public void UpdatePlayersCollectionChangeHandlers()
    {
        foreach (var team in Room.Teams.Skip(1))
            team.Players.CollectionChanged += (s, e) => UpdateVisiblePlayers();
    }

    private void UpdateVisiblePlayers()
    {
        List<Pin> pins = new();

        var players = Room.Teams
            .FirstOrDefault(t => t.Id == Player.TeamId)?.Players
            .Where(p => p.Id != Player.Id).ToList();

        foreach(var player in players)
        {
            if (player.Locations.Count > 0)
            {
                var lastLocation = player.Locations.Last();
                var pin = new Pin
                {
                    Label = player.Name,
                    Location = new Location(lastLocation.Latitude, lastLocation.Longitude),
                };
                pins.Add(pin);
            }
        }

        VisiblePlayers = new ObservableCollection<Pin>(pins);
    }
}

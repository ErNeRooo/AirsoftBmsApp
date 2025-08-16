using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.View.ContentViews.CustomMap;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.ViewModel.MapViewModel;

public partial class MapViewModel : ObservableObject, IMapViewModel
{
    [ObservableProperty]
    ObservableRoom room;

    [ObservableProperty]
    ObservablePlayer player;

    [ObservableProperty]
    List<CustomPin> visiblePlayers = new();

    [ObservableProperty]
    ObservableCollection<MapElement> mapElements = new();

    [ObservableProperty]
    CustomPin? cursorPin;

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

        UpdateVisiblePlayers();
        UpdatePlayersCollectionChangeHandlers();

        MapElements.Add(new Circle
        {
            Center = new Location(53.1407114016577, 21.05076719059483),
            Radius = new Distance(50),
            StrokeColor = Color.FromArgb("#10B981FF"),
            StrokeWidth = 3,
            FillColor = Color.FromArgb("#10B98140")
        });
    }

    public void UpdatePlayersCollectionChangeHandlers()
    {
        foreach (var team in Room.Teams.Skip(1))
            team.Players.CollectionChanged += (s, e) => UpdateVisiblePlayers();
    }

    private void UpdateVisiblePlayers()
    {
        List<CustomPin> pins = new();
        List<MapElement> mapElements = new();

        int teamId = Player.TeamId ?? 0;

        var players = Room.Teams
            .Skip(1)
            .SelectMany(t => t.Players)
            .Where(p => p.Id != Player.Id && (p.IsDead || p.TeamId == player.TeamId))
            .ToList();

        foreach(var player in players)
        {
            if (player.Locations.Count > 0)
            {
                ObservableLocation lastLocation = player.Locations.Last();

                CustomPin pin = new()
                {
                    Label = player.Name,
                    Location = new Location(lastLocation.Latitude, lastLocation.Longitude),
                    IconSource = player.IsDead ? "dead_player" : "ally_player",
                    Type = PinType.Generic,
                    ClickedCommand = MapPinClickedCommand,
                };

                Circle circle = new()
                {
                    Center = new Location(lastLocation.Latitude, lastLocation.Longitude),
                    Radius = new Distance(lastLocation.Accuracy),
                    StrokeColor = Color.FromArgb("#FF10B981"),
                    StrokeWidth = 3,
                    FillColor = Color.FromArgb("#4010B981")
                };

                mapElements.Add(circle);
                pins.Add(pin);
            }
        }

        MapElements = new ObservableCollection<MapElement>(mapElements);
        VisiblePlayers = pins;
    }

    [RelayCommand]
    private void MapPinClicked(CustomPin pin)
    {

    }

    [RelayCommand]
    private void PingMarkClicked(CustomPin pin)
    {

    }

    public void MapClicked(object sender, MapClickedEventArgs e)
    {
        

        CustomPin pin = new()
        {
            Location = e.Location,
            IconSource = "swords_icon",
            Type = PinType.Place,
            ClickedCommand = PingMarkClickedCommand,
        };

        Circle circle = new()
        {
            Center = e.Location,
            Radius = new Distance(50),
            StrokeColor = Color.FromArgb("#10B981FF"),
            StrokeWidth = 3,
            FillColor = Color.FromArgb("#10B98140")
        };

        var newList = new List<CustomPin>(VisiblePlayers);
        
        newList.Remove(CursorPin);
        newList.Add(pin);
        
        CursorPin = pin;
        
        VisiblePlayers = newList;
    }
}

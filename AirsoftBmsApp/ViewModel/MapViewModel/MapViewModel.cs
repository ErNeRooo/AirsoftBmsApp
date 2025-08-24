using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Model.Dto.Location;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.View.ContentViews.CustomMap;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace AirsoftBmsApp.ViewModel.MapViewModel;

public partial class MapViewModel : ObservableObject, IMapViewModel
{
    IApiFacade _apiFacade;

    [ObservableProperty]
    ObservableRoom room;

    [ObservableProperty]
    ObservablePlayer player;

    [ObservableProperty]
    List<CustomPin> mapPins = new();

    [ObservableProperty]
    List<MapElement> mapElements = new();

    [ObservableProperty]
    ObservableActionDialogState actionDialogState;

    [ObservableProperty]
    CustomPin? cursorPin;

    [ObservableProperty]
    CustomPin? selectedPlayerPin;

    [ObservableProperty]
    bool isLoading = false;

    [ObservableProperty]
    string errorMessage = "";

    public MapViewModel(IRoomDataService roomDataService, IPlayerDataService playerDataService, IApiFacade apiFacade)
    {
        _apiFacade = apiFacade;
        Room = roomDataService.Room;
        Player = playerDataService.Player;
        ActionDialogState = new ObservableActionDialogState(null, Player);

        Player.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(ObservablePlayer.TeamId))
            {
                UpdateMap();
                UpdatePlayersCollectionChangeHandlers();
            }
        };

        Room.Teams.CollectionChanged += (s, e) =>
        {
            UpdateMap();
            UpdatePlayersCollectionChangeHandlers();
        };

        UpdateMap();
        UpdatePlayersCollectionChangeHandlers();
    }

    public void UpdatePlayersCollectionChangeHandlers()
    {
        foreach (var team in Room.Teams.Skip(1))
        {
            team.Players.CollectionChanged += (s, e) =>
            {
                UpdateMap();
                UpdatePlayersCollectionChangeHandlers();
            };

            foreach (var player in team.Players)
            {
                player.Locations.CollectionChanged += (s, e) => UpdateMap();
                player.Deaths.CollectionChanged += (s, e) => UpdateMap();
                player.Kills.CollectionChanged += (s, e) => UpdateMap();
                player.PropertyChanged += (s, e) => UpdateMap();
            }
        }
    }

    private void UpdateMap()
    {
        List<CustomPin> pins = new();
        List<MapElement> elements = new();

        (List<CustomPin> pinsWithPlayerLocations, List<MapElement> elementsWithPlayerLocations) = AddPlayerLocations(pins, elements);
        List<CustomPin> pinsWithEnemyPings = MapEnemyPings(pinsWithPlayerLocations);

        MapPins = pinsWithEnemyPings;
        MapElements = elementsWithPlayerLocations;
    }

    private (List<CustomPin> updatedPins, List<MapElement> updatedElements) AddPlayerLocations(List<CustomPin> pins, List<MapElement> elements)
    {
        var players = Room.Teams
            .Skip(1)
            .SelectMany(t => t.Players)
            .Where(p => p.Id != Player.Id && (p.IsDead || p.TeamId == Player.TeamId))
            .ToList();

        foreach (var player in players)
        {
            if (player.Locations.Count > 0)
            {
                ObservableLocation lastLocation = player.Locations.Where(l => l.Type == "player-location").Last();

                CustomPin pin = new()
                {
                    Label = player.Name,
                    Location = new Location(lastLocation.Latitude, lastLocation.Longitude),
                    IconSource = player.IsDead ? "dead_player" : "ally_player",
                    Type = PinType.Generic,
                    ClickedCommand = PlayerPinClickedCommand,
                };

                Circle circle = new()
                {
                    Center = new Location(lastLocation.Latitude, lastLocation.Longitude),
                    Radius = new Distance(lastLocation.Accuracy),
                    StrokeColor = Color.FromArgb("#FF10B981"),
                    StrokeWidth = 3,
                    FillColor = Color.FromArgb("#4010B981")
                };

                pins.Add(pin);
                elements.Add(circle);
            }
        }

        return (pins, elements);
    }

    private List<CustomPin> MapEnemyPings(List<CustomPin> pins)
    {
        List<ObservableLocation> enemyPingLocations = Room.Teams
            .Skip(1)
            .SelectMany(t => t.Players)
            .Where(p => p.TeamId == Player.TeamId)
            .SelectMany(p => p.Locations)
            .Where(l => l.Type == "enemy-ping")
            .ToList();

        foreach(ObservableLocation location in enemyPingLocations)
        {
            CustomPin pin = new()
            {
                Label = "Enemy Spotted",
                Location = new Location(location.Latitude, location.Longitude),
                IconSizeInPixels = 60,
                IconSource = "enemy_ping_icon",
                ClickedCommand = EnemyPingClickedCommand,
            };

            pins.Add(pin);
        }

        return pins;
    }

    [RelayCommand]
    private void PlayerPinClicked(CustomPin pin)
    {
        SelectedPlayerPin = pin;
    }

    public void MapClicked(object sender, MapClickedEventArgs e)
    {
        CustomPin pin = new()
        {
            Location = e.Location,
            IconSource = "crosshair_icon",
            IconSizeInPixels = 60,
            Type = PinType.Place,
            ClickedCommand = CursorPinClickedCommand,
        };

        var newList = new List<CustomPin>(MapPins);

        newList.Remove(CursorPin);
        newList.Add(pin);

        CursorPin = pin;

        MapPins = newList;
    }

    [RelayCommand]
    private void CursorPinClicked(CustomPin pin)
    {
        ActionDialogState.IsVisible = true;
        ActionDialogState.SelectedPlayerPin = SelectedPlayerPin;
    }

    [RelayCommand]
    public async Task MarkEnemy()
    {
        IsLoading = true;
        await Task.Yield();

        PostLocationDto postLocationDto = new()
        {
            Longitude = CursorPin.Location.Longitude,
            Latitude = CursorPin.Location.Latitude,
            Accuracy = CursorPin.Location.Accuracy ?? 0,
            Bearing = CursorPin.Location.Course ?? 0,
            Time = DateTimeOffset.Now,
            Type = "enemy-ping"
        };

        var result = await _apiFacade.Location.Create(postLocationDto);

        switch (result)
        {
            case Success:
                break;
            case Failure failure:
                ErrorMessage = failure.errorMessage;
                break;
            case Error error:
                ErrorMessage = error.errorMessage;
                break;
            default:
                throw new InvalidOperationException("Unknown result type");
        }

        ActionDialogState.IsVisible = false;
        IsLoading = false;
    }

    [RelayCommand]
    public async Task OrderMove()
    {
        var a = Room.Teams.SelectMany(t => t.Players).ToList().FirstOrDefault(p => p.Name == SelectedPlayerPin?.Label);

        a.Locations.Add(new ObservableLocation()
        {
            Latitude = CursorPin.Location.Latitude,
            Longitude = CursorPin.Location.Longitude,
            Accuracy = CursorPin.Location.Accuracy ?? 0,
            Bearing = CursorPin.Location.Course ?? 0,
            Time = DateTimeOffset.Now,
            Type = "player-location"
        });
    }

    [RelayCommand]
    public async Task OrderDefend()
    {

    }

    [RelayCommand]
    public async Task AddSpawnZone()
    {

    }

    [RelayCommand]
    public async Task EnemyPingClicked()
    {

    }

    [RelayCommand]
    public async Task ReportKill()
    {
        IsLoading = true;
        await Task.Yield();

        Location? playersLocation = await Geolocation.GetLocationAsync();

        if (playersLocation is null)
        {
            ErrorMessage = AppResources.LocationNotAvailableErrorMessage;
            IsLoading = false;
            return;
        }

        PostKillDto postKillDto = new()
        {
            Longitude = playersLocation.Longitude,
            Latitude = playersLocation.Latitude,
            Accuracy = playersLocation.Accuracy ?? 0,
            Bearing = playersLocation.Course ?? 0,
            Time = DateTimeOffset.Now
        };

        var result = await _apiFacade.Kill.Create(postKillDto);

        switch (result)
        {
            case Success:
                break;
            case Failure failure:
                ErrorMessage = failure.errorMessage;
                break;
            case Error error:
                ErrorMessage = error.errorMessage;
                break;
            default:
                throw new InvalidOperationException("Unknown result type");
        }

        IsLoading = false;
    }

    [RelayCommand]
    public async Task ReportDeath()
    {
        IsLoading = true;
        await Task.Yield();

        Location? playersLocation = await Geolocation.GetLocationAsync();

        if (playersLocation is null)
        {
            ErrorMessage = AppResources.LocationNotAvailableErrorMessage;
            IsLoading = false;
            return;
        }

        PostDeathDto postDeathDto = new()
        {
            Longitude = playersLocation.Longitude,
            Latitude = playersLocation.Latitude,
            Accuracy = playersLocation.Accuracy ?? 0,
            Bearing = playersLocation.Course ?? 0,
            Time = DateTimeOffset.Now
        };

        var result = await _apiFacade.Death.Create(postDeathDto);

        switch (result)
        {
            case Success:
                break;
            case Failure failure:
                ErrorMessage = failure.errorMessage;
                break;
            case Error error:
                ErrorMessage = error.errorMessage;
                break;
            default:
                throw new InvalidOperationException("Unknown result type");
        }

        IsLoading = false;
    }
}

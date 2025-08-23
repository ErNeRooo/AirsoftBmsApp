using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Model.Dto.Kills;
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
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.ViewModel.MapViewModel;

public partial class MapViewModel : ObservableObject, IMapViewModel
{
    IApiFacade _apiFacade;

    [ObservableProperty]
    ObservableRoom room;

    [ObservableProperty]
    ObservablePlayer player;

    [ObservableProperty]
    List<CustomPin> visiblePlayers = new();

    [ObservableProperty]
    ObservableCollection<MapElement> mapElements = new();

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

                mapElements.Add(circle);
                pins.Add(pin);
            }
        }

        MapElements = new ObservableCollection<MapElement>(mapElements);
        VisiblePlayers = pins;
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
            Type = PinType.Place,
            ClickedCommand = CursorPinClickedCommand,
        };

        var newList = new List<CustomPin>(VisiblePlayers);

        newList.Remove(CursorPin);
        newList.Add(pin);

        CursorPin = pin;

        VisiblePlayers = newList;
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
    
    }

    [RelayCommand]
    public async Task OrderMove()
    {

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

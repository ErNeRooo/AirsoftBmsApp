using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Model.Dto.Location;
using AirsoftBmsApp.Model.Dto.Order;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Utils;
using AirsoftBmsApp.View.ContentViews.CustomMap;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MethodTimer;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using System.Collections;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;

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
    ObservableCollection<MapElement> mapElements = new();

    [ObservableProperty]
    Polygon? zoneSelection;

    [ObservableProperty]
    ObservableActionDialogState actionDialogState;

    [ObservableProperty]
    ObservableConfirmationDialogState confirmationDialogState = new();

    [ObservableProperty]
    ObservableCreateSpawnZoneDialogState createSpawnZoneDialogState;

    [ObservableProperty]
    CustomPin? cursorPin;

    [ObservableProperty]
    ObservablePlayer? selectedPlayer;

    [ObservableProperty]
    bool isLoading = false;

    [ObservableProperty]
    string errorMessage = "";

    [ObservableProperty]
    string informationMessage = "";

    [ObservableProperty]
    bool isSpawnCreationMode = false;

    public MapViewModel(IRoomDataService roomDataService, IPlayerDataService playerDataService, IApiFacade apiFacade)
    {
        _apiFacade = apiFacade;
        Room = roomDataService.Room;
        Player = playerDataService.Player;
        ActionDialogState = new ObservableActionDialogState(null, Player);
        CreateSpawnZoneDialogState = new ObservableCreateSpawnZoneDialogState(Room.Teams);

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

    [Time]
    public void UpdatePlayersCollectionChangeHandlers()
    {
        foreach (var team in Room.Teams.Skip(1))
        {
            team.Players.CollectionChanged -= Players_CollectionChanged;
            team.Players.CollectionChanged += Players_CollectionChanged;

            foreach (var player in team.Players)
            {
                player.Locations.CollectionChanged -= (s, e) => UpdateMap();
                player.Deaths.CollectionChanged -= (s, e) => UpdateMap();
                player.Kills.CollectionChanged -= (s, e) => UpdateMap();
                player.PropertyChanged -= (s, e) => UpdateMap();

                player.Locations.CollectionChanged += (s, e) => UpdateMap();
                player.Deaths.CollectionChanged += (s, e) => UpdateMap();
                player.Kills.CollectionChanged += (s, e) => UpdateMap();
                player.PropertyChanged += (s, e) => UpdateMap();
            }
        }
    }

    private void Players_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        UpdateMap();
        UpdatePlayersCollectionChangeHandlers();
    }

    [Time]
    private void UpdateMap()
    {
        List<CustomPin> pins = new();
        List<MapElement> elements = new();

        (List<CustomPin> pinsWithPlayerLocations, List<MapElement> elementsWithPlayerLocations) = AddPlayerLocations(pins, elements);
        (List<CustomPin> pinsWithOrders, List<MapElement> elementsWithOrders) = AddOrders(pinsWithPlayerLocations, elementsWithPlayerLocations);
        List<CustomPin> pinsWithEnemyPings = AddEnemyPings(pinsWithOrders);
        List<MapElement> mapElementsWithSpawnZones = AddSpawnZones(elementsWithOrders);
        (List<CustomPin> updatedPins, List<MapElement> updatedElements) = AddSelectionZone(pinsWithEnemyPings, mapElementsWithSpawnZones);
        if(CursorPin is not null) updatedPins.Add(CursorPin);

        MapPins = updatedPins;
        MapElements = new(updatedElements);
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

                pins.Add(pin);
                elements.Add(circle);
            }
        }

        return (pins, elements);
    }

    private (List<CustomPin> updatedPins, List<MapElement> updatedElements) AddOrders(List<CustomPin> pins, List<MapElement> elements)
    {
        List<ObservableOrder>? orders = Room.Teams
            .FirstOrDefault(t => t.Id == Player.TeamId)?.Orders
            .Where(o => o.Type == OrderTypes.MOVE || o.Type == OrderTypes.DEFEND).ToList();

        if(orders is null || orders.Count == 0) return (pins, elements);

        foreach (var order in orders)
        {
            ObservablePlayer? player = Room.Teams
                .SelectMany(t => t.Players)
                .FirstOrDefault(p => p.Id == order.PlayerId);

            ObservableLocation playerLastLocation = player.Locations.Last();

            Location playerLocation = new Location(playerLastLocation.Latitude, playerLastLocation.Longitude);
            Location orderLocation = new Location(order.Latitude, order.Longitude);

            CustomPin pin = new()
            {
                Location = new Location(order.Latitude, order.Longitude),
                IconSource = order.Type == OrderTypes.MOVE ? "move_icon" : "defend_icon",
                HorizontalAnchor = 0.5f,
                VerticalAnchor = 1f,
                IconSizeInPixels = 60,
                Type = PinType.Generic,
                DataObject = order,
                ClickedCommand = DeleteOrderConfirmationCommand,
            };

            Polyline polyline = new()
            {
                StrokeColor = Color.FromArgb("#FFCA3B33"),
                StrokeWidth = 5,
                Geopath =
                {
                    playerLocation,
                    orderLocation
                }
            };

            pins.Add(pin);
            elements.Add(polyline);
        }
        
        return (pins, elements);
    }

    private List<CustomPin> AddEnemyPings(List<CustomPin> pins)
    {
        List<ObservableOrder>? enemyPings = Room.Teams
            .FirstOrDefault(t => t.Id == Player.TeamId)?.Orders
            .Where(l => l.Type == OrderTypes.MARKENEMY)
            .ToList();

        foreach(ObservableOrder order in enemyPings ?? [])
        {
            CustomPin pin = new()
            {
                Label = "Enemy Spotted",
                Location = new Location(order.Latitude, order.Longitude),
                IconSizeInPixels = 60,
                IconSource = "enemy_ping_icon",
                ClickedCommand = EnemyPingClickedCommand,
                DataObject = order,
            };

            pins.Add(pin);
        }

        return pins;
    }

    private List<MapElement> AddSpawnZones(List<MapElement> mapElements)
    {
        var updated = new List<MapElement>(mapElements);

        List<Polygon?> spawns = Room.Teams
            .Where(t => t.SpawnZone is not null)
            .Select(t => t.SpawnZone)
            .ToList();

        if (spawns.Count == 0) return updated;

        foreach (Polygon zone in spawns)
        {
            Polygon newPolygon = new()
            {
                StrokeColor = zone.StrokeColor,
                FillColor = zone.FillColor,
            };

            foreach(Location location in zone.Geopath)
            {
                newPolygon.Geopath.Add(location);
            }

            updated.Add(newPolygon);
        }

        return updated;
    }

    private (List<CustomPin> updatedPins, List<MapElement> updatedElements) AddSelectionZone(List<CustomPin> pins, List<MapElement> elements)
    {
        if (ZoneSelection is null) return (pins, elements);

        foreach (Location location in ZoneSelection.Geopath)
        {
            pins.Add(new CustomPin()
            {
                Location = location,
                VerticalAnchor = 1.0f,
                IconSource = "zone_vertex_icon",
                IconSizeInPixels = 40,
                Type = PinType.Place,
                ClickedCommand = ZoneVertexClickedCommand,
            });
        }

        elements.Add(ZoneSelection);

        return (pins, elements);
    }

    [RelayCommand]
    private void ZoneVertexClicked(CustomPin pin)
    {
        Location? vertexToRemove = ZoneSelection.Geopath.FirstOrDefault(
            location => location.Latitude == pin.Location.Latitude 
            && location.Longitude == pin.Location.Longitude);

        if (vertexToRemove is not null)
        {
            Polygon polygon = GetNewPolygon();
            polygon.Geopath.Remove(vertexToRemove);

            ZoneSelection = polygon;
            UpdateMap();
        }
    }

    [RelayCommand]
    private void PlayerPinClicked(CustomPin pin)
    {
        var players = Room.Teams.SelectMany(t => t.Players).ToList();

        ObservablePlayer? player = players.FirstOrDefault(
            p => p.Locations.LastOrDefault()?.Latitude == pin.Location.Latitude
            && p.Locations.LastOrDefault()?.Longitude == pin.Location.Longitude
            && p.Name == pin.Label);
        
        if (player is not null)
        {
            SelectedPlayer = player;
        }
    }

    public void MapClicked(object sender, MapClickedEventArgs e)
    {
        if(IsSpawnCreationMode && ZoneSelection?.Count >= 20) InformationMessage = AppResources.MaxZoneVerticesReachedMessage;
        else if (IsSpawnCreationMode) AddZoneVertex(e);
        else ChangeCursorPosition(e);
    }
    public void AddZoneVertex(MapClickedEventArgs e)
    {
        Polygon polygon = GetNewPolygon();
        polygon.Geopath.Add(e.Location);

        ZoneSelection = polygon;

        UpdateMap();
    }

    public Polygon GetNewPolygon()
    {
        Polygon polygon = new()
        {
            StrokeColor = Color.FromArgb("#FF3B82F6"),
            StrokeWidth = 3,
            FillColor = Color.FromArgb("#403B82F6"),
        };

        foreach (var location in ZoneSelection?.Geopath ?? [])
        {
            polygon.Geopath.Add(location);
        }

        return polygon;
    }

    public void ChangeCursorPosition(MapClickedEventArgs e)
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
        ActionDialogState.SelectedPlayer = SelectedPlayer;
    }

    [RelayCommand]
    public async Task MarkEnemy()
    {
        IsLoading = true;
        await Task.Yield();

        PostOrderDto postOrderDto = new()
        {
            PlayerId = Player.Id,
            Longitude = CursorPin.Location.Longitude,
            Latitude = CursorPin.Location.Latitude,
            Accuracy = CursorPin.Location.Accuracy ?? 0,
            Bearing = CursorPin.Location.Course ?? 0,
            Time = DateTimeOffset.Now,
            Type = OrderTypes.MARKENEMY
        };

        var result = await _apiFacade.Order.Create(postOrderDto);

        switch (result)
        {
            case Success:
                CursorPin = null;
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

        UpdateMap();
        ActionDialogState.IsVisible = false;
        IsLoading = false;
    }

    [RelayCommand]
    public async Task Respawn()
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

        Polygon? spawnZone = Room.Teams.FirstOrDefault(t => t.Id == Player.TeamId)?.SpawnZone;
        bool canRespawn = spawnZone is null ? true : spawnZone.IsPointInPolygon(playersLocation);

        if (!canRespawn)
        {
            InformationMessage = AppResources.CannotRespawnWhenOutsideSpawnZoneErrorMessage;
            IsLoading = false;
            return;
        }

        PutPlayerDto putPlayerDto = new()
        {
            IsDead = false
        };

        var result = await _apiFacade.Player.Update(putPlayerDto, Player.Id);

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
    public async Task OrderMove()
    {
        if (SelectedPlayer is null) return;

        IsLoading = true;
        await Task.Yield();

        if (CursorPin?.Location is null)
        {
            ErrorMessage = AppResources.LocationNotAvailableErrorMessage;
            IsLoading = false;
            return;
        }

        PostOrderDto postOrderDto = new()
        {
            PlayerId = SelectedPlayer.Id,
            Longitude = CursorPin.Location.Longitude,
            Latitude = CursorPin.Location.Latitude,
            Accuracy = CursorPin.Location.Accuracy ?? 0,
            Bearing = CursorPin.Location.Course ?? 0,
            Time = DateTimeOffset.Now,
            Type = OrderTypes.MOVE
        };

        var result = await _apiFacade.Order.Create(postOrderDto);

        switch (result)
        {
            case Success:
                CursorPin = null;
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

        UpdateMap();
        ActionDialogState.IsVisible = false;
        IsLoading = false;
    }

    [RelayCommand]
    public async Task OrderDefend()
    {
        if (SelectedPlayer is null) return;

        IsLoading = true;
        await Task.Yield();

        if (CursorPin?.Location is null)
        {
            ErrorMessage = AppResources.LocationNotAvailableErrorMessage;
            IsLoading = false;
            return;
        }

        PostOrderDto postOrderDto = new()
        {
            PlayerId = SelectedPlayer.Id,
            Longitude = CursorPin.Location.Longitude,
            Latitude = CursorPin.Location.Latitude,
            Accuracy = CursorPin.Location.Accuracy ?? 0,
            Bearing = CursorPin.Location.Course ?? 0,
            Time = DateTimeOffset.Now,
            Type = OrderTypes.DEFEND
        };

        var result = await _apiFacade.Order.Create(postOrderDto);

        switch (result)
        {
            case Success:
                CursorPin = null;
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

        UpdateMap();
        ActionDialogState.IsVisible = false;
        IsLoading = false;
    }

    [RelayCommand]
    public async Task DeleteOrderConfirmation(CustomPin pin)
    {
        if (pin.DataObject is not ObservableOrder order) return;

        ConfirmationDialogState.Message = AppResources.DeleteOrderConfirmationMessage;
        ConfirmationDialogState.Command = new AsyncRelayCommand(async () => await DeleteOrder(order));
    }

    [RelayCommand]
    public async Task DeleteOrder(ObservableOrder order)
    {
        IsLoading = true;
        await Task.Yield();

        var result = await _apiFacade.Order.Delete(order.OrderId);

        switch (result)
        {
            case Success:
                UpdateMap();
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

        ConfirmationDialogState.Message = "";
        IsLoading = false;
    }

    [RelayCommand]
    public async Task SaveSpawnZone()
    {
        if (ZoneSelection is null || ZoneSelection.Geopath.Count < 1) return;

        IsLoading = true;
        await Task.Yield();

        PutTeamDto teamDto = new()
        {
            SpawnZoneVertices = ZoneSelection.Geopath.Select(location => new PostLocationDto()
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude,
                Accuracy = location.Accuracy ?? 0,
                Bearing = location.Course ?? 0,
                Time = DateTimeOffset.Now,
            }).ToArray()
        };

        var result = await _apiFacade.Team.Update(teamDto, CreateSpawnZoneDialogState.SelectedTeam.Id);

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

        ZoneSelection = null;
        UpdateMap();
        IsSpawnCreationMode = false;
        IsLoading = false;
    }

    [RelayCommand]
    public async Task ShowCreateSpawnDialog()
    {
        CreateSpawnZoneDialogState.Teams = new(Room.Teams.Skip(1));
        CreateSpawnZoneDialogState.IsVisible = true;
    }

    [RelayCommand]
    public async Task HideCreateSpawnDialog()
    {
        CreateSpawnZoneDialogState.IsVisible = false;
    }

    [RelayCommand]
    public async Task TurnOnSpawnCreationMode()
    {
        IsSpawnCreationMode = true;
        CursorPin = null;
        UpdateMap();
        CreateSpawnZoneDialogState.IsVisible = false;
        ActionDialogState.IsVisible = false;
    }

    [RelayCommand]
    public async Task TurnOffSpawnCreationMode()
    {
        ZoneSelection = null;
        UpdateMap();
        IsSpawnCreationMode = false;
    }

    [RelayCommand]
    public async Task EnemyPingClicked(CustomPin pin)
    {
        if (pin.DataObject is not ObservableOrder ping) return;

        ConfirmationDialogState.Message = AppResources.DeleteEnemyPingConfirmationMessage;
        ConfirmationDialogState.Command = new AsyncRelayCommand(async () => await DeleteOrder(ping));
    }

    [RelayCommand]
    public async Task DeleteLocation(ObservableLocation location)
    {
        IsLoading = true;
        await Task.Yield();

        var result = await _apiFacade.Location.Delete(location.LocationId);

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

        ConfirmationDialogState.Message = "";
        IsLoading = false;
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

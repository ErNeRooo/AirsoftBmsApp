using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.GeolocationService;
using AirsoftBmsApp.Services.HubConnectionService;
using AirsoftBmsApp.Services.HubNotificationHandlerService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Validation;
using AirsoftBmsApp.Validation.Rules;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MethodTimer;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AirsoftBmsApp.ViewModel.BattleViewModel;

public partial class BattleViewModel : ObservableObject, IBattleViewModel
{
    private readonly IApiFacade _apiFacade;
    private readonly IHubConnectionService _hubConnectionService;

    [ObservableProperty]
    ObservablePlayer player;

    [ObservableProperty]
    ObservableRoom room;

    [ObservableProperty]
    ObservableCollection<ObservableTeamSummary> teamSummaries = new();

    [ObservableProperty]
    ObservableConfirmationDialogState confirmationDialogState = new();

    [ObservableProperty]
    ObservableBattleSettingsState battleSettingsState;

    [ObservableProperty]
    bool isLoading = false;

    [ObservableProperty]
    string errorMessage = "";

    public ValidatableObject<string> CreateBattleName { get; set; } = new();

    public BattleViewModel(
        IValidationHelperFactory validationHelperFactory,
        IPlayerDataService playerDataService, 
        IRoomDataService roomDataService,
        IApiFacade apiFacade,
        IHubConnectionService hubConnection,
        IHubNotificationHandlerService notificationHandlers
        )
    {
        _hubConnectionService = hubConnection;
        BattleSettingsState = new ObservableBattleSettingsState(validationHelperFactory);
        _apiFacade = apiFacade;
        Player = playerDataService.Player;
        Room = roomDataService.Room;

        Room.Teams.CollectionChanged += Teams_CollectionChanged;

        UpdatePlayersCollectionChangeHandlers();
        RebuildTeamSummaries();

        CreateBattleName.Validations.Add(new HasMaxLengthRule<string>
        {
            ValidationMessage = string.Format(AppResources.BattleNameIsTooShortValidationMessage, 60),
            MaxLength = 60
        });

        CreateBattleName.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = AppResources.BattleNameIsRequiredValidationMessage,
        });

        SetNotificationHandlers(notificationHandlers);
    }

    void SetNotificationHandlers(IHubNotificationHandlerService notificationHandlers)
    {
        _hubConnectionService.HubConnection.On<int>(
            HubNotifications.BattleDeleted,
            (battleId) => notificationHandlers.Battle.OnBattleDeleted(battleId, Room)
        );
        _hubConnectionService.HubConnection.On<BattleDto>(
            HubNotifications.BattleUpdated,
            (battleDto) => notificationHandlers.Battle.OnBattleUpdated(battleDto, Room)
        );
        _hubConnectionService.HubConnection.On<BattleDto>(
            HubNotifications.BattleCreated,
            (battleDto) => notificationHandlers.Battle.OnBattleCreated(battleDto, Room)
        );
    }

    [Time]
    private void RebuildTeamSummaries()
    {
        TeamSummaries.Clear();
        foreach (var t in Room.Teams.Skip(1))
            TeamSummaries.Add(new ObservableTeamSummary(t));

        if (TeamSummaries.Any())
            TeamSummaries.Last().IsLast = true;
    }

    [Time]
    public void UpdatePlayersCollectionChangeHandlers()
    {
        foreach (var team in Room.Teams.Skip(1))
        {
            team.PropertyChanged -= Team_PropertyChanged;
            team.PropertyChanged += Team_PropertyChanged;

            team.Players.CollectionChanged -= Players_CollectionChanged;
            team.Players.CollectionChanged += Players_CollectionChanged;

            foreach (var player in team.Players)
            {
                player.Deaths.CollectionChanged -= PlayerDeathsChanged;
                player.Deaths.CollectionChanged += PlayerDeathsChanged;
            }
        }
    }

    private void Team_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        RebuildTeamSummaries();
    }

    [Time]
    private void Teams_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        RebuildTeamSummaries();
        UpdatePlayersCollectionChangeHandlers();
    }

    [Time]
    private void Players_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        RebuildTeamSummaries();
        UpdatePlayersCollectionChangeHandlers();
    }

    [Time]
    private void PlayerDeathsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        RebuildTeamSummaries();
    }

    [RelayCommand]
    public async Task CreateBattle()
    {
        ValidateBattleName();
        if (!CreateBattleName.IsValid) return;

        IsLoading = true;
        await Task.Yield();

        PostBattleDto postBattleDto = new()
        {
            Name = CreateBattleName.Value,
            RoomId = Room.Id
        };

        var result = await _apiFacade.Battle.Create(postBattleDto);

        switch (result)
        {
            case Success:
                ErrorMessage = "";
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
    public async Task EndBattleConfirmation()
    {
        ConfirmationDialogState.Message = AppResources.EndBattleConfirmationDialogMessage;
        ConfirmationDialogState.Command = EndBattleCommand;
    }

    [RelayCommand]
    public async Task EndBattle()
    {
        IsLoading = true;
        await Task.Yield();

        var result = await _apiFacade.Battle.End(Room.Battle.BattleId);

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

        BattleSettingsState.IsVisible = false;
        ConfirmationDialogState.Message = "";
        IsLoading = false;
    }

    [RelayCommand]
    public async Task UpdateBattle()
    {
        BattleSettingsState.ValidateBattleName();
        if (!BattleSettingsState.BattleForm.Name.IsValid) return;

        IsLoading = true;
        await Task.Yield();

        PutBattleDto putBattleDto = new()
        {
            Name = BattleSettingsState.BattleForm.Name.Value,
            IsActive = BattleSettingsState.IsActive
        };

        var result = await _apiFacade.Battle.Update(putBattleDto, Room.Battle.BattleId);

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

        BattleSettingsState.IsVisible = false;
        IsLoading = false;
    }

    [RelayCommand]
    public async Task ValidateBattleName()
    {
        CreateBattleName.Validate();
    }

    [RelayCommand]
    public async Task ShowBattleSettings()
    {
        if (!Player.IsAdmin) return;

        BattleSettingsState.BattleForm.Name.Value = "";
        BattleSettingsState.IsActive = Room.Battle.IsActive;
        BattleSettingsState.IsVisible = true;
    }
}


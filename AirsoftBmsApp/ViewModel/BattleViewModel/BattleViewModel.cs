using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Validation;
using AirsoftBmsApp.Validation.Rules;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.ViewModel.BattleViewModel;

public partial class BattleViewModel : ObservableObject, IBattleViewModel
{
    IApiFacade _apiFacade;

    [ObservableProperty]
    ObservablePlayer player;

    [ObservableProperty]
    ObservableRoom room;

    [ObservableProperty]
    ObservableCollection<ObservableTeamSummary> teamSummaries = new();

    [ObservableProperty]
    bool isLoading = false;

    [ObservableProperty]
    string errorMessage = "";

    public ValidatableObject<string> BattleName { get; set; } = new();

    public BattleViewModel(
        IPlayerDataService playerDataService, 
        IRoomDataService roomDataService,
        IApiFacade apiFacade
        )
    {
        _apiFacade = apiFacade;

        Player = playerDataService.Player;
        Room = roomDataService.Room;

        Room.Teams.CollectionChanged += (s, e) =>
        {
            UpdatePlayersCollectionChangeHandlers();
            RebuildTeamSummaries();
        };

        UpdatePlayersCollectionChangeHandlers();
        RebuildTeamSummaries();

        BattleName.Validations.Add(new HasMaxLengthRule<string>
        {
            ValidationMessage = string.Format(AppResources.BattleNameIsTooShortValidationMessage, 60),
            MaxLength = 60
        });

        BattleName.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = AppResources.BattleNameIsRequiredValidationMessage,
        });
    }

    private void RebuildTeamSummaries()
    {
        TeamSummaries.Clear();
        foreach (var t in Room.Teams.Skip(1))
            TeamSummaries.Add(new ObservableTeamSummary(t));

        if (TeamSummaries.Any())
            TeamSummaries.Last().IsLast = true;
    }

    public void UpdatePlayersCollectionChangeHandlers()
    {
        foreach (var team in Room.Teams.Skip(1))
            team.Players.CollectionChanged += (s, e) => RebuildTeamSummaries();
    }

    [RelayCommand]
    public async Task CreateBattle()
    {
        ValidateBattleName();
        if (!BattleName.IsValid) return;

        IsLoading = true;

        PostBattleDto postBattleDto = new()
        {
            Name = BattleName.Value,
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
    public async Task ValidateBattleName()
    {
        BattleName.Validate();
    }
}


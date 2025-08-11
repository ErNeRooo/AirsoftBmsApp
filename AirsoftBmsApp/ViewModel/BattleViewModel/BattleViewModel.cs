using AirsoftBmsApp.Model.Observable;
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
    [ObservableProperty]
    ObservablePlayer player;

    [ObservableProperty]
    ObservableRoom room;

    [ObservableProperty]
    ObservableCollection<ObservableTeamSummary> teamSummaries = new();

    public ValidatableObject<string> Name { get; set; } = new();

    public BattleViewModel(IPlayerDataService playerDataService, IRoomDataService roomDataService)
    {
        Player = playerDataService.Player;
        Room = roomDataService.Room;

        Room.Teams.CollectionChanged += (s, e) =>
        {
            UpdatePlayersCollectionChangeHandlers();
            RebuildTeamSummaries();
        };

        UpdatePlayersCollectionChangeHandlers();
        RebuildTeamSummaries();

        Name.Validations.Add(new HasMaxLengthRule<string>
        {
            ValidationMessage = string.Format(AppResources.BattleNameIsTooShortValidationMessage, 60),
            MaxLength = 60
        });

        Name.Validations.Add(new IsNotNullOrEmptyRule<string>
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
        if (!Name.IsValid) return;
    }

    [RelayCommand]
    public async Task ValidateBattleName()
    {
        Name.Validate();
    }
}


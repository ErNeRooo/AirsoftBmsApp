using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservablePlayerProfileState : ObservableObject
{
    [ObservableProperty]
    private ObservablePlayer selectedPlayer;

    [ObservableProperty]
    private ObservablePlayer selfPlayer;

    [ObservableProperty]
    private ObservableCollection<ObservableTeam> teams;

    [ObservableProperty]
    private ObservableTeam selectedTeam;

    [ObservableProperty]
    private string teamName;

    [ObservableProperty]
    private bool isOfficerOfSelectedPlayer = false;

    [ObservableProperty]
    private bool isAccountLinked = false;

    [ObservableProperty]
    private bool isVisible = false;

    [ObservableProperty]
    private bool isSelfSelected = false;

    partial void OnSelectedPlayerChanged(ObservablePlayer value)
    {
        if (value.TeamId == SelfPlayer.TeamId && SelfPlayer.IsOfficer) IsOfficerOfSelectedPlayer = true;
        else IsOfficerOfSelectedPlayer = false;

        IsAccountLinked = value.Account is not null;

        IsSelfSelected = value.Id == SelfPlayer.Id;
    }

    partial void OnTeamsChanged(ObservableCollection<ObservableTeam> value)
    {
        ObservableTeam? playersTeam = value.FirstOrDefault(team => team.Id == SelectedPlayer.TeamId);

        if (playersTeam is null) TeamName = "Under No Flag";
        else TeamName = playersTeam.Name;

        int selectedPlayersTeamId = SelectedPlayer.TeamId ?? 0;
        SelectedTeam = value.FirstOrDefault(team => team.Id == selectedPlayersTeamId) ?? new ObservableTeam
        {
            Id = 0,
            Name = "Under No Flag"
        };
    }
}

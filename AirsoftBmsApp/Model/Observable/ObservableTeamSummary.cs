using AirsoftBmsApp.Resources.Styles.TeamTheme;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableTeamSummary : ObservableObject
{
    [ObservableProperty]
    string teamName;

    [ObservableProperty]
    int deathsCount;

    [ObservableProperty]
    int playerCount;

    [ObservableProperty]
    bool isLast = false;

    [ObservableProperty]
    ITeamTheme teamTheme = TeamThemes.UnderNoFlag;

    public ObservableTeamSummary()
    {
        
    }

    public ObservableTeamSummary(ObservableTeam team)
    {
        TeamName = team.Name;
        PlayerCount = team.Players.Count();
        DeathsCount = team.Players.SelectMany(player => player.Deaths).Count();
    }
}

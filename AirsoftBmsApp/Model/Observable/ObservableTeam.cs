using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Resources.Styles.TeamTheme;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableTeam : ObservableObject
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private int roomId;

    [ObservableProperty]
    private int? officerId;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private ObservableCollection<ObservablePlayer> players = new();

    [ObservableProperty]
    private ITeamTheme teamTheme = TeamThemes.UnderNoFlag;

    public ObservableTeam()
    {
        
    }

    public ObservableTeam(TeamDto team)
    {
        Id = team.TeamId;
        RoomId = team.RoomId;
        OfficerId = team.OfficerPlayerId;
        Name = team.Name;
    }
}

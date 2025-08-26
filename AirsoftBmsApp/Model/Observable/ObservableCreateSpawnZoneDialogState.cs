using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableCreateSpawnZoneDialogState : ObservableObject
{
    [ObservableProperty]
    private bool isVisible = false;

    [ObservableProperty]
    private ObservableCollection<ObservableTeam> teams = new();

    [ObservableProperty]
    private ObservableTeam? selectedTeam;

    public ObservableCreateSpawnZoneDialogState(ObservableCollection<ObservableTeam> teams)
    {
        Teams = new(teams.Skip(1).ToList());

        if (teams.Count > 0) SelectedTeam = Teams[0];
    }
}

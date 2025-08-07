using AirsoftBmsApp.Model.Dto.Player;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservablePlayer : ObservableObject, IObservablePlayer
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private bool isDead;

    [ObservableProperty]
    private ObservableAccount? account;

    [ObservableProperty]
    private ObservableCollection<ObservableDeath> deaths = new();

    [ObservableProperty]
    private int? teamId;

    [ObservableProperty]
    private int? roomId;

    [ObservableProperty]
    private bool isAdmin = false;

    [ObservableProperty]
    private bool isOfficer = false;

    public ObservablePlayer()
    {
        
    }

    public ObservablePlayer(PlayerDto playerDto)
    {
        Id = playerDto.PlayerId;
        Name = playerDto.Name;
        IsDead = playerDto.IsDead;
        TeamId = playerDto.TeamId;
        RoomId = playerDto.RoomId;
    }

    public void UpdateIsAdmin(IObservableRoom room)
    {
        if (room.AdminPlayerId == Id) IsAdmin = true;
        else IsAdmin = false;
    }

    public void UpdateIsOfficer(IObservableTeam team)
    {
        if (team.OfficerId == Id) IsOfficer = true;
        else IsOfficer = false;
    }
}


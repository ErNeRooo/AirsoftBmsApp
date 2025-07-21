using AirsoftBmsApp.Model.Dto.Player;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
    private int? teamId;

    [ObservableProperty]
    private int? roomId;

    [ObservableProperty]
    private bool isAdmin = false;

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

    public void Update(IObservableRoom subject)
    {
        if (subject.AdminPlayerId == Id) IsAdmin = true;
        else IsAdmin = false;
    }
}


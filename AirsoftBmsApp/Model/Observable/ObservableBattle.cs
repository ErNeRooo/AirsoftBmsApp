using AirsoftBmsApp.Model.Dto.Battle;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableBattle : ObservableObject
{
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private int battleId;

    [ObservableProperty]
    private bool isActive;

    [ObservableProperty]
    private int roomId;

    [ObservableProperty]
    public Action? onBattleActivated;

    [ObservableProperty]
    public Action? onBattleDeactivated;

    public ObservableCollection<ObservableZone> Zones { get; set; } = new();

    public ObservableBattle()
    {
    }

    public ObservableBattle(BattleDto battleDto)
    {
        Name = battleDto.Name;
        BattleId = battleDto.BattleId;
        IsActive = battleDto.IsActive;
        RoomId = battleDto.RoomId;
    }

    public ObservableBattle(BattleDto battleDto, Action OnBattleActivated, Action OnBattleDeactivated)
    {
        this.OnBattleActivated = OnBattleActivated;
        this.OnBattleDeactivated = OnBattleDeactivated;
        Name = battleDto.Name;
        BattleId = battleDto.BattleId;
        IsActive = battleDto.IsActive;
        RoomId = battleDto.RoomId;
    }

    partial void OnIsActiveChanged(bool value)
    {
        if (value && OnBattleActivated is not null) OnBattleActivated();
        else if (!value && OnBattleDeactivated is not null) OnBattleDeactivated();
    }
}

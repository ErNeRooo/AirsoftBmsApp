using AirsoftBmsApp.Model.Dto.Kills;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableKill : ObservableObject
{
    [ObservableProperty]
    private int killId;

    [ObservableProperty]
    private int locationId;

    [ObservableProperty]
    private int playerId;

    [ObservableProperty]
    private int battleId;

    [ObservableProperty]
    private double longitude;

    [ObservableProperty]
    private double latitude;

    [ObservableProperty]
    private double accuracy;

    [ObservableProperty]
    private double bearing;

    [ObservableProperty]
    private DateTimeOffset time;

    public ObservableKill(KillDto killDto)
    {
        KillId = killDto.KillId;
        LocationId = killDto.LocationId;
        PlayerId = killDto.PlayerId;
        BattleId = killDto.BattleId;
        Longitude = killDto.Longitude;
        Latitude = killDto.Latitude;
        Accuracy = killDto.Accuracy;
        Bearing = killDto.Bearing;
        Time = killDto.Time;
    }
}

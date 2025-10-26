using AirsoftBmsApp.Model.Dto.Death;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableDeath : ObservableObject
{
    [ObservableProperty]
    private int deathId;

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

    public ObservableDeath()
    {
        
    }
    public ObservableDeath(DeathDto deathDto)
    {
        DeathId = deathDto.DeathId;
        PlayerId = deathDto.PlayerId;
        BattleId = deathDto.BattleId;
        Longitude = deathDto.Longitude;
        Latitude = deathDto.Latitude;
        Accuracy = deathDto.Accuracy;
        Bearing = deathDto.Bearing;
        Time = deathDto.Time;
    }
}

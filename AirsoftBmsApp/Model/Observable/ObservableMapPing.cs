using AirsoftBmsApp.Model.Dto.MapPing;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableMapPing : ObservableObject
{
    [ObservableProperty]
    private int mapPingId;

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

    public ObservableMapPing()
    {
        
    }
    public ObservableMapPing(MapPingDto mapPingDto)
    {
        MapPingId = mapPingDto.MapPingId;
        PlayerId = mapPingDto.PlayerId;
        BattleId = mapPingDto.BattleId;
        Longitude = mapPingDto.Longitude;
        Latitude = mapPingDto.Latitude;
        Accuracy = mapPingDto.Accuracy;
        Bearing = mapPingDto.Bearing;
        Time = mapPingDto.Time;
    }
}

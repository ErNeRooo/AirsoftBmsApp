using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Model.Dto.Location;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableLocation : ObservableObject
{

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
    private Int16 bearing;

    [ObservableProperty]
    private DateTimeOffset time;

    public ObservableLocation(LocationDto locationDto)
    {
        LocationId = locationDto.LocationId;
        PlayerId = locationDto.PlayerId;
        BattleId = locationDto.BattleId;
        Longitude = locationDto.Longitude;
        Latitude = locationDto.Latitude;
        Accuracy = locationDto.Accuracy;
        Bearing = locationDto.Bearing;
        Time = locationDto.Time;
    }
}

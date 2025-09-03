using AirsoftBmsApp.Model.Dto.Order;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableOrder : ObservableObject
{

    [ObservableProperty]
    private int orderId;

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

    [ObservableProperty]
    private string type;

    public ObservableOrder()
    {
        
    }
    public ObservableOrder(OrderDto orderDto)
    {
        OrderId = orderDto.OrderId;
        PlayerId = orderDto.PlayerId;
        BattleId = orderDto.BattleId;
        Longitude = orderDto.Longitude;
        Latitude = orderDto.Latitude;
        Accuracy = orderDto.Accuracy;
        Bearing = orderDto.Bearing;
        Time = orderDto.Time;
        Type = orderDto.Type;
    }
}

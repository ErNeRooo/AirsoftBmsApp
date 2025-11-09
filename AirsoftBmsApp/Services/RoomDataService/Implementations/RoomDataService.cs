using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.RoomDataService.Implementations;

public class RoomDataService : IRoomDataService
{
    private ObservableRoom _room = new();
    public ObservableRoom Room
    {
        get => _room;
        set
        {
            _room = value;
            RoomChanged?.Invoke(this, _room);
        }
    }

    public event EventHandler<ObservableRoom>? RoomChanged;
}

using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.RoomDataService.Abstractions;

public interface IRoomDataService
{
    ObservableRoom Room { get; set; }

    public event EventHandler<ObservableRoom>? RoomChanged;
}

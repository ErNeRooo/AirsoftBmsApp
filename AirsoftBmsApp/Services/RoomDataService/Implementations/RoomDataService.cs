using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.RoomDataService.Implementations;

public class RoomDataService : IRoomDataService
{
    public ObservableRoom Room { get; set; } = new ObservableRoom();
}

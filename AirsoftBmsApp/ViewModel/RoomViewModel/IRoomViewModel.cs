using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.ViewModel.RoomViewModel;

public interface IRoomViewModel
{
    ObservableRoom Room { get; set; }
    public Task LeaveRoom();
}

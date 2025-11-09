using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.PlayerDataService.Abstractions;

public interface IPlayerDataService
{
    ObservablePlayer Player { get; set; }
    public event EventHandler<ObservablePlayer>? PlayerChanged;
}

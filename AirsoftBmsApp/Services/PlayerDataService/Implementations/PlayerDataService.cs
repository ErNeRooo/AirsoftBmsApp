using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;


namespace AirsoftBmsApp.Services.PlayerDataService.Implementations;

public class PlayerDataService : IPlayerDataService
{
    public ObservablePlayer Player { get; set; } = new();
}

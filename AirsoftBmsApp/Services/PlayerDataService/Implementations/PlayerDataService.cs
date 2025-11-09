using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Services.PlayerDataService.Implementations;

public partial class PlayerDataService : IPlayerDataService 
{

    private ObservablePlayer _player = new();
    public ObservablePlayer Player
    {
        get => _player;
        set
        {
            _player = value;
            PlayerChanged?.Invoke(this, _player);
        }
    }

    public event EventHandler<ObservablePlayer>? PlayerChanged;
}

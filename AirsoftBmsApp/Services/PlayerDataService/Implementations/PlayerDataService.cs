using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Services.PlayerDataService.Implementations;

public partial class PlayerDataService : IPlayerDataService 
{
    public ObservablePlayer Player { get; set; } = new();
}

using AirsoftBmsApp.View.ContentViews.CustomMap;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableActionDialogState(ObservablePlayer? _selectedPlayer, ObservablePlayer _selfPlayer) : ObservableObject
{
    [ObservableProperty]
    private bool isVisible = false;

    [ObservableProperty]
    private ObservablePlayer? selectedPlayer = _selectedPlayer;

    [ObservableProperty]
    private ObservablePlayer selfPlayer = _selfPlayer;
}

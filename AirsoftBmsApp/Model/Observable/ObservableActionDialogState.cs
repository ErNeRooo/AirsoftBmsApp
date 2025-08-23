using AirsoftBmsApp.View.ContentViews.CustomMap;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableActionDialogState(CustomPin _selectedPlayerPin, ObservablePlayer _player) : ObservableObject
{
    [ObservableProperty]
    private bool isVisible = false;

    [ObservableProperty]
    private CustomPin? selectedPlayerPin = _selectedPlayerPin;

    [ObservableProperty]
    private ObservablePlayer player = _player;
}

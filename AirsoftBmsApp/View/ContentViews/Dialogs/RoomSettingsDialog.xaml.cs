using AirsoftBmsApp.Model.Observable;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Dialogs;

public partial class RoomSettingsDialog : ContentView
{
    public static readonly BindableProperty RoomSettingsProperty =
        BindableProperty.Create(nameof(RoomSettings), typeof(ObservableRoomSettingsState), typeof(RoomSettingsDialog));

    public static readonly BindableProperty UpdateRoomCommandProperty =
        BindableProperty.Create(nameof(UpdateRoomCommand), typeof(ICommand), typeof(RoomSettingsDialog));

    public static readonly BindableProperty DeleteRoomCommandProperty =
        BindableProperty.Create(nameof(DeleteRoomCommand), typeof(ICommand), typeof(RoomSettingsDialog));

    public ObservableRoomSettingsState RoomSettings
    {
        get => (ObservableRoomSettingsState)GetValue(RoomSettingsProperty);
        set => SetValue(RoomSettingsProperty, value);
    }

    public ICommand UpdateRoomCommand
    {
        get => (ICommand)GetValue(UpdateRoomCommandProperty);
        set => SetValue(UpdateRoomCommandProperty, value);
    }

    public ICommand DeleteRoomCommand
    {
        get => (ICommand)GetValue(DeleteRoomCommandProperty);
        set => SetValue(DeleteRoomCommandProperty, value);
    }

    public RoomSettingsDialog()
	{
		InitializeComponent();
	}

    public async void Cancel(object sender, EventArgs e)
    {
        RoomSettings.IsVisible = false;
    }
}
using AirsoftBmsApp.Model.Observable;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Dialogs;

public partial class PlayerProfileDialog : ContentView
{
    int _pickerIndexBeforeFocus = -1;

    public static readonly BindableProperty PlayerProfileStateProperty =
        BindableProperty.Create(nameof(PlayerProfileState), typeof(ObservablePlayerProfileState), typeof(PlayerProfileDialog));

    public static readonly BindableProperty KickFromRoomCommandProperty =
        BindableProperty.Create(nameof(KickFromRoomCommand), typeof(ICommand), typeof(PlayerProfileDialog));

    public static readonly BindableProperty KickFromTeamCommandProperty =
        BindableProperty.Create(nameof(KickFromTeamCommand), typeof(ICommand), typeof(PlayerProfileDialog));

    public static readonly BindableProperty MoveToAnotherTeamCommandProperty =
        BindableProperty.Create(nameof(MoveToAnotherTeamCommand), typeof(ICommand), typeof(PlayerProfileDialog));

    public ObservablePlayerProfileState PlayerProfileState
    {
        get => (ObservablePlayerProfileState)GetValue(PlayerProfileStateProperty);
        set => SetValue(PlayerProfileStateProperty, value);
    }

    public ICommand KickFromRoomCommand
    {
        get => (ICommand)GetValue(KickFromRoomCommandProperty);
        set => SetValue(KickFromRoomCommandProperty, value);
    }

    public ICommand KickFromTeamCommand
    {
        get => (ICommand)GetValue(KickFromTeamCommandProperty);
        set => SetValue(KickFromTeamCommandProperty, value);
    }

    public ICommand MoveToAnotherTeamCommand
    {
        get => (ICommand)GetValue(MoveToAnotherTeamCommandProperty);
        set => SetValue(MoveToAnotherTeamCommandProperty, value);
    }

    public void Hide(object sender, EventArgs e)
    {
        PlayerProfileState.IsVisible = false;
    }

    public void TeamPickerFocused(object? sender, FocusEventArgs e)
    {
        _pickerIndexBeforeFocus = TeamPicker.SelectedIndex;
    }

    public void TeamPickerUnfocused(object? sender, FocusEventArgs e)
    {
        if (TeamPicker.SelectedIndex != _pickerIndexBeforeFocus)
        {
            if (MoveToAnotherTeamCommand?.CanExecute(PlayerProfileState.SelectedTeam) == true)
                MoveToAnotherTeamCommand.Execute(PlayerProfileState.SelectedTeam);
        }

        _pickerIndexBeforeFocus = -1;
    }

    public PlayerProfileDialog()
	{
		InitializeComponent();
	}
}
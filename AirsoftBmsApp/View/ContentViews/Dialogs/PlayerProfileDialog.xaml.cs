using AirsoftBmsApp.Model.Observable;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Dialogs;

public partial class PlayerProfileDialog : ContentView
{
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

    public void Cancel(object sender, EventArgs e)
    {
        PlayerProfileState.IsVisible = false;
    }

    public void SelectionChanged(object sender, EventArgs e)
    {
        if (MoveToAnotherTeamCommand?.CanExecute(PlayerProfileState.SelectedTeam) == true)
        {
            MoveToAnotherTeamCommand.Execute(PlayerProfileState.SelectedTeam);
        }
    }

    public PlayerProfileDialog()
	{
		InitializeComponent();
	}
}
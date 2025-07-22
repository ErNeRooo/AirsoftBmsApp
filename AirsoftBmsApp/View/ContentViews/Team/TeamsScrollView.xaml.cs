using AirsoftBmsApp.Model.Observable;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Team;

public partial class TeamsScrollView : ContentView
{
	public static readonly BindableProperty TeamsProperty =
        BindableProperty.Create(nameof(Teams), typeof(ObservableCollection<ObservableTeam>), typeof(TeamsScrollView), default(ObservableCollection<ObservableTeam>));

    public static readonly BindableProperty IsCreateTeamButtonVisibleProperty =
        BindableProperty.Create(nameof(IsCreateTeamButtonVisible), typeof(bool), typeof(TeamsScrollView), false);

    public static readonly BindableProperty CreateTeamButtonCommandProperty =
        BindableProperty.Create(nameof(CreateTeamButtonCommand), typeof(ICommand), typeof(TeamsScrollView));

    public static readonly BindableProperty SwitchTeamCommandProperty =
        BindableProperty.Create(nameof(SwitchTeamCommand), typeof(ICommand), typeof(TeamsScrollView));

    public static readonly BindableProperty TakeOfficerCommandProperty =
        BindableProperty.Create(nameof(TakeOfficerCommand), typeof(ICommand), typeof(TeamsScrollView));

    public static readonly BindableProperty ShowSettingsCommandProperty =
        BindableProperty.Create(nameof(ShowSettingsCommand), typeof(ICommand), typeof(TeamsScrollView));

    public static readonly BindableProperty PlayerProperty =
    BindableProperty.Create(nameof(Player), typeof(ObservablePlayer), typeof(TeamsScrollView));

    public ObservablePlayer Player
    {
        get => (ObservablePlayer)GetValue(PlayerProperty);
        set => SetValue(PlayerProperty, value);
    }

    public ObservableCollection<ObservableTeam> Teams
	{
		get => (ObservableCollection<ObservableTeam>)GetValue(TeamsProperty); 
		set => SetValue(TeamsProperty, value);
    }

    public bool IsCreateTeamButtonVisible
    {
        get => (bool)GetValue(IsCreateTeamButtonVisibleProperty);
        set => SetValue(IsCreateTeamButtonVisibleProperty, value);
    }

    public ICommand CreateTeamButtonCommand
    {
        get => (ICommand)GetValue(CreateTeamButtonCommandProperty);
        set => SetValue(CreateTeamButtonCommandProperty, value);
    }

    public ICommand SwitchTeamCommand
    {
        get => (ICommand)GetValue(SwitchTeamCommandProperty);
        set => SetValue(SwitchTeamCommandProperty, value);
    }

    public ICommand TakeOfficerCommand
    {
        get => (ICommand)GetValue(TakeOfficerCommandProperty);
        set => SetValue(TakeOfficerCommandProperty, value);
    }

    public ICommand ShowSettingsCommand
    {
        get => (ICommand)GetValue(ShowSettingsCommandProperty);
        set => SetValue(ShowSettingsCommandProperty, value);
    }

    public TeamsScrollView()
	{
		InitializeComponent();
	}
}
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

    public TeamsScrollView()
	{
		InitializeComponent();
	}
}
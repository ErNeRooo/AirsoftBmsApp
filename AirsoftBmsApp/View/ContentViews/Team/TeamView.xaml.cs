using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Utils;
using System.ComponentModel;

namespace AirsoftBmsApp.View.ContentViews.Team;

public partial class TeamView : VerticalStackLayout
{
    public static readonly BindableProperty IsOfficerProperty =
        BindableProperty.Create(nameof(IsOfficer), typeof(bool), typeof(TeamView), default(bool));

    public bool IsOfficer
    {
        get => (bool)GetValue(IsOfficerProperty);
        set => SetValue(IsOfficerProperty, value);
    }

    private ObservableTeam? _team;

    public TeamView()
	{
		InitializeComponent();

        BindingContextChanged += OnBindingContextChanged;
    }

    private void OnBindingContextChanged(object? sender, EventArgs e)
    {
        _team = BindingContext as ObservableTeam;

        if (_team is not null)
            _team.PropertyChanged += OnTeamPropertyChanged;
    }

    private void OnTeamPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ObservableTeam.OfficerId))
            UpdateIsOfficer();
    }

    private void UpdateIsOfficer()
    {
        if (BindingContext is not ObservableTeam team) return;

        var teamsScrollView = VisualTreeHelper.FindParent<TeamsScrollView>(this);

        if (teamsScrollView is null) return;

        if (teamsScrollView.Player.Id == team.OfficerId) IsOfficer = true;
        else IsOfficer = false;
    }
}
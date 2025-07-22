using AirsoftBmsApp.Model.Observable;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Dialogs;

public partial class TeamSettingsDialog : ContentView
{

    public static readonly BindableProperty TeamSettingsProperty =
        BindableProperty.Create(nameof(TeamSettings), typeof(ObservableTeamSettingsState), typeof(TeamSettingsDialog), default(ObservableTeamSettingsState));

    public static readonly BindableProperty UpdateTeamCommandProperty =
        BindableProperty.Create(nameof(UpdateTeamCommand), typeof(ICommand), typeof(TeamSettingsDialog));

    public static readonly BindableProperty DeleteTeamCommandProperty =
        BindableProperty.Create(nameof(DeleteTeamCommand), typeof(ICommand), typeof(TeamSettingsDialog));

    public ICommand DeleteTeamCommand
    {
        get => (ICommand)GetValue(DeleteTeamCommandProperty);
        set => SetValue(DeleteTeamCommandProperty, value);
    }

    public ICommand UpdateTeamCommand
    {
        get => (ICommand)GetValue(UpdateTeamCommandProperty);
        set => SetValue(UpdateTeamCommandProperty, value);
    }

    public ObservableTeamSettingsState TeamSettings
    {
        get => (ObservableTeamSettingsState)GetValue(TeamSettingsProperty);
        set => SetValue(TeamSettingsProperty, value);
    }

    public TeamSettingsDialog()
	{
		InitializeComponent();
	}

    public async void Cancel(object sender, EventArgs e)
    {
        IsVisible = false;
    }
}
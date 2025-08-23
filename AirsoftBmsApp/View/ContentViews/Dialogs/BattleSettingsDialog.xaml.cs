using AirsoftBmsApp.Model.Observable;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Dialogs;

public partial class BattleSettingsDialog : ContentView
{
    public static readonly BindableProperty BattleSettingsProperty =
        BindableProperty.Create(nameof(BattleSettings), typeof(ObservableBattleSettingsState), typeof(BattleSettingsDialog));

    public static readonly BindableProperty UpdateBattleCommandProperty =
        BindableProperty.Create(nameof(UpdateBattleCommand), typeof(ICommand), typeof(BattleSettingsDialog));

    public static readonly BindableProperty EndBattleCommandProperty =
        BindableProperty.Create(nameof(EndBattleCommand), typeof(ICommand), typeof(BattleSettingsDialog));

    public ObservableBattleSettingsState BattleSettings
    {
        get => (ObservableBattleSettingsState)GetValue(BattleSettingsProperty);
        set => SetValue(BattleSettingsProperty, value);
    }

    public ICommand UpdateBattleCommand
    {
        get => (ICommand)GetValue(UpdateBattleCommandProperty);
        set => SetValue(UpdateBattleCommandProperty, value);
    }

    public ICommand EndBattleCommand
    {
        get => (ICommand)GetValue(EndBattleCommandProperty);
        set => SetValue(EndBattleCommandProperty, value);
    }

    public BattleSettingsDialog()
	{
		InitializeComponent();
	}

    public async void Cancel(object sender, EventArgs e)
    {
        BattleSettings.IsVisible = false;
    }
}
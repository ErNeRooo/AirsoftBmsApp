using AirsoftBmsApp.Model.Observable;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Dialogs;

public partial class MapActionDialog : ContentView
{
    public static readonly BindableProperty ActionDialogStateProperty =
        BindableProperty.Create(nameof(ActionDialogState), typeof(ObservableActionDialogState), typeof(BattleSettingsDialog));

    public static readonly BindableProperty MarkEnemyCommandProperty =
        BindableProperty.Create(nameof(MarkEnemyCommand), typeof(ICommand), typeof(BattleSettingsDialog));

    public static readonly BindableProperty OrderToMoveCommandProperty =
        BindableProperty.Create(nameof(OrderToMoveCommand), typeof(ICommand), typeof(BattleSettingsDialog));

    public static readonly BindableProperty OrderToDefendCommandProperty =
        BindableProperty.Create(nameof(OrderToDefendCommand), typeof(ICommand), typeof(BattleSettingsDialog));

    public static readonly BindableProperty AddSpawnZoneCommandProperty =
        BindableProperty.Create(nameof(AddSpawnZoneCommand), typeof(ICommand), typeof(BattleSettingsDialog));

    public ObservableActionDialogState ActionDialogState
    {
        get => (ObservableActionDialogState)GetValue(ActionDialogStateProperty);
        set => SetValue(ActionDialogStateProperty, value);
    }

    public ICommand MarkEnemyCommand
    {
        get => (ICommand)GetValue(MarkEnemyCommandProperty);
        set => SetValue(MarkEnemyCommandProperty, value);
    }
    public ICommand OrderToMoveCommand
    {
        get => (ICommand)GetValue(OrderToMoveCommandProperty);
        set => SetValue(OrderToMoveCommandProperty, value);
    }
    public ICommand OrderToDefendCommand
    {
        get => (ICommand)GetValue(OrderToDefendCommandProperty);
        set => SetValue(OrderToDefendCommandProperty, value);
    }
    public ICommand AddSpawnZoneCommand
    {
        get => (ICommand)GetValue(AddSpawnZoneCommandProperty);
        set => SetValue(AddSpawnZoneCommandProperty, value);
    }

    public MapActionDialog()
	{
		InitializeComponent();
	}

    public async void Cancel(object sender, EventArgs e)
    {
        ActionDialogState.IsVisible = false;
    }
}
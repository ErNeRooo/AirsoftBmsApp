using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.ViewModel.MapViewModel;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Dialogs;

public partial class CreateSpawnZoneDialog : ContentView
{
	public static readonly BindableProperty CreateSpawnZoneDialogStateProperty = BindableProperty.Create(
		nameof(CreateSpawnZoneDialogState),
		typeof(ObservableCreateSpawnZoneDialogState),
		typeof(CreateSpawnZoneDialog),
		defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty BackCommandProperty = BindableProperty.Create(
		nameof(BackCommand),
		typeof(ICommand),
		typeof(CreateSpawnZoneDialog));

    public static readonly BindableProperty OkCommandProperty = BindableProperty.Create(
		nameof(OkCommand),
		typeof(ICommand),
		typeof(CreateSpawnZoneDialog));

    public ObservableCreateSpawnZoneDialogState CreateSpawnZoneDialogState
    {
		get => (ObservableCreateSpawnZoneDialogState)GetValue(CreateSpawnZoneDialogStateProperty);
		set => SetValue(CreateSpawnZoneDialogStateProperty, value);
    }

	public ICommand BackCommand
	{
		get => (ICommand)GetValue(BackCommandProperty);
		set => SetValue(BackCommandProperty, value);
    }

	public ICommand OkCommand
    {
		get => (ICommand)GetValue(OkCommandProperty);
		set => SetValue(OkCommandProperty, value);
    }

    public CreateSpawnZoneDialog()
	{
		InitializeComponent();
	}
}
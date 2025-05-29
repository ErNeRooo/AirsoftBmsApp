using AirsoftBmsApp.Validation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Team;

public partial class CreateTeamDialog : ContentView
{
    public static readonly BindableProperty CreateTeamCommandProperty =
    BindableProperty.Create(nameof(CreateTeamCommand), typeof(ICommand), typeof(TeamsScrollView));

    public static readonly BindableProperty CancelCommandProperty =
        BindableProperty.Create(nameof(CancelCommand), typeof(ICommand), typeof(TeamsScrollView));

    public static readonly BindableProperty ValidatableNameProperty =
    BindableProperty.Create(nameof(ValidatableName), typeof(ValidatableObject<string>), typeof(TeamsScrollView));

    public static readonly BindableProperty ValidateCommandProperty =
    BindableProperty.Create(nameof(ValidateCommand), typeof(ICommand), typeof(CreateTeamDialog));

    public ValidatableObject<string> ValidatableName
    {
        get => (ValidatableObject<string>)GetValue(ValidatableNameProperty);
        set => SetValue(ValidatableNameProperty, value);
    }

    public ICommand CreateTeamCommand
    {
        get => (ICommand)GetValue(CreateTeamCommandProperty);
        set => SetValue(CreateTeamCommandProperty, value);
    }

    public ICommand CancelCommand
    {
        get => (ICommand)GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }

    public ICommand ValidateCommand
    {
        get => (ICommand)GetValue(ValidateCommandProperty);
        set => SetValue(ValidateCommandProperty, value);
    }

    public CreateTeamDialog()
	{
		InitializeComponent();
	}
}
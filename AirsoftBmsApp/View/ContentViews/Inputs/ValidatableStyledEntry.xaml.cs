using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Validation;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Inputs;

public partial class ValidatableStyledEntry : ContentView
{
    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(StyledEntry), AppResources.TextInputPlaceholder);

    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(StyledEntry), false);

    public static readonly BindableProperty ValidateCommandProperty =
        BindableProperty.Create(nameof(ValidateCommand), typeof(ICommand), typeof(StyledEntry));

    public static readonly BindableProperty ValidatableObjectProperty =
        BindableProperty.Create(nameof(ValidatableObject), typeof(ValidatableObject<string>), typeof(StyledEntry));

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public ValidatableObject<string> ValidatableObject
    {
        get => (ValidatableObject<string>)GetValue(ValidatableObjectProperty);
        set => SetValue(ValidatableObjectProperty, value);
    }

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }
    public ICommand ValidateCommand
    {
        get => (ICommand)GetValue(ValidateCommandProperty);
        set => SetValue(ValidateCommandProperty, value);
    }

    public ValidatableStyledEntry()
    {
        InitializeComponent();
    }
}
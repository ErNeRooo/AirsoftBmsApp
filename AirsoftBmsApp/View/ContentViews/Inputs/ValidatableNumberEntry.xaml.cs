using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Validation;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Inputs;

public partial class ValidatableNumberEntry : ContentView
{
    public static readonly BindableProperty PlaceholderProperty =
    BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(StyledEntry), AppResources.TextInputPlaceholder);

    public static readonly BindableProperty ValidateCommandProperty =
        BindableProperty.Create(nameof(ValidateCommand), typeof(ICommand), typeof(StyledEntry));

    public static readonly BindableProperty ValidatableObjectProperty =
        BindableProperty.Create(nameof(ValidatableObject), typeof(ValidatableObject<int>), typeof(StyledEntry));

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public ValidatableObject<int> ValidatableObject
    {
        get => (ValidatableObject<int>)GetValue(ValidatableObjectProperty);
        set => SetValue(ValidatableObjectProperty, value);
    }

    public ICommand ValidateCommand
    {
        get => (ICommand)GetValue(ValidateCommandProperty);
        set => SetValue(ValidateCommandProperty, value);
    }

    public ValidatableNumberEntry()
	{
		InitializeComponent();
	}

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;

        if (!IsDigitsOnly(e.NewTextValue))
        {
            entry.Text = e.OldTextValue;
        }
    }

    private bool IsDigitsOnly(string text)
    {
        return text.All(char.IsDigit);
    }
}
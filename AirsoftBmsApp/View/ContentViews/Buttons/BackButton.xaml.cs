using AirsoftBmsApp.Resources.Languages;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Buttons;

public partial class BackButton : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(OutlinedButton), AppResources.BackButton);

    public static readonly BindableProperty CommandProperty = 
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(BackButton));

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(string), typeof(BackButton), "..");

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public BackButton()
	{
		InitializeComponent();
    }
}
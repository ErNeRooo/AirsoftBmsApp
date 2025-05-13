using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Buttons;

public partial class OutlinedButton : ContentView
{
    public event EventHandler Clicked;

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(OutlinedButton), "Other");

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FilledButton));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    } 

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public OutlinedButton()
    {
        InitializeComponent();
        button.Clicked += (s, e) => Clicked?.Invoke(this, e);
    }
}
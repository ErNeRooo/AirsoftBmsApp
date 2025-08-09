using AirsoftBmsApp.Resources.Languages;

namespace AirsoftBmsApp.View.ContentViews.Inputs;

public partial class StyledEntry : ContentView
{
    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(StyledEntry), AppResources.TextInputPlaceholder);

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(StyledEntry), defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(StyledEntry), false);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public StyledEntry()
    {
        InitializeComponent();
    }
}
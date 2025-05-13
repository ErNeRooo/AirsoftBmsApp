namespace AirsoftBmsApp.View.ContentViews.Forms;

public partial class StyledEntry : ContentView
{
    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(StyledEntry), "Enter Text");

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public StyledEntry()
	{
		InitializeComponent();
	}
}
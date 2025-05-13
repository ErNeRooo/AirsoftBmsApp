namespace AirsoftBmsApp.View.ContentViews.Text;

public partial class Header : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(Header), "Header");

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public Header()
	{
		InitializeComponent();
	}
}
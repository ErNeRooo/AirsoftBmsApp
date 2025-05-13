namespace AirsoftBmsApp.View.ContentViews.Text;

public partial class ResizableLabelWithHorizontalLine : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(Header), "OR");

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ResizableLabelWithHorizontalLine()
	{
		InitializeComponent();
	}
}
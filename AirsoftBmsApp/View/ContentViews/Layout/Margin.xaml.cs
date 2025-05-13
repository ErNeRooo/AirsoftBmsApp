namespace AirsoftBmsApp.View.ContentViews.Layout;

public partial class Margin : ContentView
{
    public static readonly BindableProperty SizeProperty =
        BindableProperty.Create(nameof(Size), typeof(int), typeof(Margin), 0);

    public int Size
    {
        get => (int)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }
    public Margin()
	{
		InitializeComponent();
	}
}
namespace AirsoftBmsApp.View.ContentViews.Buttons;

public partial class BackButton : ContentView
{
    public static readonly BindableProperty PathProperty = BindableProperty.Create(
        nameof(Path), typeof(string), typeof(BackButton), "..");

    public string Path
    {
        get => (string)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }

    public BackButton()
	{
		InitializeComponent();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(Path);
    }
}
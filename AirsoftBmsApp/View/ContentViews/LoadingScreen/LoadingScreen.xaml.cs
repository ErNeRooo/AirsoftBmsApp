using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.View.ContentViews.LoadingScreen;

public partial class LoadingScreen : ContentView
{
	public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(nameof(IsLoading), typeof(string), typeof(LoadingScreen), "false");

    public string IsLoading
    {
        get => (string)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public LoadingScreen()
	{
		InitializeComponent();
	}
}
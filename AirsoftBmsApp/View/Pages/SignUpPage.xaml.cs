namespace AirsoftBmsApp.View.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}

    private async void OnCreateButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RoomFormPage));
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
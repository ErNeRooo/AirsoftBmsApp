namespace AirsoftBmsApp.View.Pages;

public partial class CreateRoomPage : ContentPage
{
	public CreateRoomPage()
	{
		InitializeComponent();
	}

    private async void OnCreateButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RoomMembersPage));
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
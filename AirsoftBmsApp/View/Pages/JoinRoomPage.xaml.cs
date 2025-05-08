namespace AirsoftBmsApp.View.Pages;

public partial class JoinRoomPage : ContentPage
{
	public JoinRoomPage()
	{
		InitializeComponent();
	}

    private async void OnJoinButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RoomMembersPage));
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
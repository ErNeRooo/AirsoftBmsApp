using AirsoftBmsApp.View.Pages;

namespace AirsoftBmsApp
{
    public partial class RoomFormPage : ContentPage
    {
        public RoomFormPage()
        {
            InitializeComponent();
        }

        private async void OnJoinRoomButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(JoinRoomPage));
        }

        private async void OnCreateRoomButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CreateRoomPage));
        }

        private async void OnLogOutButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(PlayerFormPage)}");
        }
    }
}


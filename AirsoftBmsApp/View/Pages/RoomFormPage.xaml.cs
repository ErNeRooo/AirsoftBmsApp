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
            //await Shell.Current.GoToAsync(nameof(LogInPage));
        }

        private async void OnCreateRoomButtonClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync(nameof(SignUpPage));
        }

        private async void OnLogOutButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(PlayerFormPage)}");
        }
    }
}


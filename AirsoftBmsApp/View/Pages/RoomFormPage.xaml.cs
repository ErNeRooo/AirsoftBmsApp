using System.Windows.Input;
using AirsoftBmsApp.View.Pages;

namespace AirsoftBmsApp
{
    public partial class RoomFormPage : ContentPage
    {
        public RoomFormPage()
        {
            InitializeComponent();
        }

        public async void OnJoinRoomButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(JoinRoomPage));
        }

        public async void OnCreateRoomButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CreateRoomPage));
        }
    }
}


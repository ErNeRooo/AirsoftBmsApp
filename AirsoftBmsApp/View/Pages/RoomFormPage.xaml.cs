using System.Windows.Input;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.View.Pages;
using AirsoftBmsApp.ViewModel.RoomFormPageViewModel;

namespace AirsoftBmsApp
{
    public partial class RoomFormPage : ContentPage
    {
        public RoomFormPage(IPlayerDataService playerDataService, IRoomFormViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Title.Text = playerDataService.Player.Name;
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


using System.Windows.Input;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.View.Pages;
using AirsoftBmsApp.ViewModel.RoomFormPageViewModel;

namespace AirsoftBmsApp
{
    public partial class RoomFormPage : ContentPage
    {
        IRoomFormViewModel _viewModel;

        public RoomFormPage(IPlayerDataService playerDataService, IRoomFormViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            Title.Text = string.Format(AppResources.WelcomePlayerHeader, playerDataService.Player.Name);
        }

        public async void OnJoinRoomButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(JoinRoomPage));
        }

        public async void OnCreateRoomButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CreateRoomPage));
        }

        protected override bool OnBackButtonPressed()
        {
            _viewModel.LogOut();
            return true;
        }
    }
}


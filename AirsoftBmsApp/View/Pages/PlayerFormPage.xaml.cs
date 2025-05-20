using AirsoftBmsApp.View.Pages;
using AirsoftBmsApp.ViewModel.PlayerFormViewModel;

namespace AirsoftBmsApp
{
    public partial class PlayerFormPage : ContentPage
    {
        public PlayerFormPage(IPlayerFormViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnLogInButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(LogInPage));
        }

        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SignUpPage));
        }
    }
}


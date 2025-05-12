using AirsoftBmsApp.View.Pages;
using AirsoftBmsApp.ViewModel.Abstractions;

namespace AirsoftBmsApp
{
    public partial class PlayerFormPage : ContentPage
    {
        public PlayerFormPage(IFormViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
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


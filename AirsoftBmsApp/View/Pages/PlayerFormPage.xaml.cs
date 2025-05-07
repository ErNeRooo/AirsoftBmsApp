using AirsoftBmsApp.View.Pages;

namespace AirsoftBmsApp
{
    public partial class PlayerFormPage : ContentPage
    {
        public PlayerFormPage()
        {
            InitializeComponent();
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


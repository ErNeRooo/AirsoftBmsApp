using AirsoftBmsApp.View.Pages;

namespace AirsoftBmsApp
{
    public partial class PlayerFormPage : ContentPage
    {
        public PlayerFormPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(LogInPage));
        }
    }

}

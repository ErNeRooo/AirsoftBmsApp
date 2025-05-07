using AirsoftBmsApp.View.Pages;

namespace AirsoftBmsApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LogInPage), typeof(LogInPage));
        }
    }
}

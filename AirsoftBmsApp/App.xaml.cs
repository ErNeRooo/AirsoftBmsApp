namespace AirsoftBmsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#if ANDROID
            App.Current.UserAppTheme = AppTheme.Dark;
#endif
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
using AirsoftBmsApp.View.Pages;

namespace AirsoftBmsApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LogInPage), typeof(LogInPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(RoomFormPage), typeof(RoomFormPage));
            Routing.RegisterRoute(nameof(JoinRoomPage), typeof(JoinRoomPage));
            Routing.RegisterRoute(nameof(CreateRoomPage), typeof(CreateRoomPage));
            Routing.RegisterRoute(nameof(RoomMembersPage), typeof(RoomMembersPage));
        }
    }
}

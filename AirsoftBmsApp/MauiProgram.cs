using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerDataService.Implementations;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Implementations;
using AirsoftBmsApp.ViewModel.CreateRoomFormViewModel;
using AirsoftBmsApp.ViewModel.JoinRoomFormViewModel;
using AirsoftBmsApp.ViewModel.PlayerFormViewModel;
using AirsoftBmsApp.ViewModel.RoomViewModel;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;


namespace AirsoftBmsApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("CascadiaCode-VariableFont_wght", "CascadiaCode");
                });

            builder.Services.AddSingleton<IRoomViewModel, RoomViewModel>();
            builder.Services.AddSingleton<IPlayerFormViewModel, PlayerFormViewModel>();
            builder.Services.AddSingleton<ICreateRoomFormViewModel, CreateRoomFormViewModel>();
            builder.Services.AddSingleton<IJoinRoomFormViewModel, JoinRoomFormViewModel>();

            builder.Services.AddSingleton<IPlayerDataService, PlayerDataService>();
            builder.Services.AddSingleton<IPlayerRestService, MockPlayerRestService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

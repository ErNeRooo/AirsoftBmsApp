using AirsoftBmsApp.Services;
using AirsoftBmsApp.Services.Abstractions;
using AirsoftBmsApp.Services.Implementations;
using AirsoftBmsApp.ViewModel;
using AirsoftBmsApp.ViewModel.Abstractions;
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

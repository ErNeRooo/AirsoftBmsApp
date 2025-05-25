using AirsoftBmsApp.Services.AccountRestService.Abstractions;
using AirsoftBmsApp.Services.AccountRestService.Implementations;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerDataService.Implementations;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Implementations;
using AirsoftBmsApp.Validation;
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
            builder.Services.AddTransient<IPlayerFormViewModel, PlayerFormViewModel>();
            builder.Services.AddTransient<ICreateRoomFormViewModel, CreateRoomFormViewModel>();
            builder.Services.AddTransient<IJoinRoomFormViewModel, JoinRoomFormViewModel>();

            builder.Services.AddSingleton<IAccountRestService, AccountRestService>();
            builder.Services.AddSingleton<IPlayerRestService, PlayerRestService>();

            builder.Services.AddSingleton<IValidationHelperFactory, ValidationHelperFactory>();
            builder.Services.AddSingleton<IPlayerDataService, PlayerDataService>();
            builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();

            builder.Services.AddHttpClient<IAccountRestService, AccountRestService>(client =>
            {
                client.BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8080/Account/" : "http://localhost:8080/Account/");
            });

            builder.Services.AddHttpClient<IPlayerRestService, PlayerRestService>(client =>
            {
                client.BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8080/Player/" : "http://localhost:8080/Player/");
            });

            builder.Services.AddHttpClient<IPlayerFormViewModel, PlayerFormViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

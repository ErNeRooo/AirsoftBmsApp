using AirsoftBmsApp.Services.AccountRestService.Abstractions;
using AirsoftBmsApp.Services.AccountRestService.Implementations;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerDataService.Implementations;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Implementations;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using AirsoftBmsApp.Services.RestHelperService.Implementations;
using AirsoftBmsApp.Services.RoomRestService.Implementations;
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

            bool isUsingMockRestServices = true; // Set to true to use mock REST services

            if (isUsingMockRestServices)
            {
                builder.Services.AddSingleton<IAccountRestService, MockAccountRestService>();
                builder.Services.AddSingleton<IPlayerRestService, MockPlayerRestService>();
                builder.Services.AddSingleton<IRoomRestService, MockRoomRestService>();
            } else
            {
                var androidBaseAddress = "http://10.0.2.2:8080/";
                var baseAddress = "http://localhost:8080/";

                builder.Services.AddHttpClient<IAccountRestService, AccountRestService>(client =>
                {
                    client.BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? $"{androidBaseAddress}/Account/" : $"{baseAddress}/Account/");
                });

                builder.Services.AddHttpClient<IPlayerRestService, PlayerRestService>(client =>
                {
                    client.BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? $"{androidBaseAddress}/Player/" : $"{baseAddress}/Player/");
                });

                builder.Services.AddHttpClient<IRoomRestService, RoomRestService>(client =>
                {
                    client.BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? $"{androidBaseAddress}/Room/" : $"{baseAddress}/Room/");
                });
            }

            builder.Services.AddSingleton<IValidationHelperFactory, ValidationHelperFactory>();
            builder.Services.AddSingleton<IPlayerDataService, PlayerDataService>();
            builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
            builder.Services.AddSingleton<IJsonHelperService, JsonHelperService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

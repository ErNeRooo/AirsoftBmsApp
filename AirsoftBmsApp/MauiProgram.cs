using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Account;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Player;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Room;
using AirsoftBmsApp.Services.AccountRestService.Abstractions;
using AirsoftBmsApp.Services.AccountRestService.Implementations;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerDataService.Implementations;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Implementations;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using AirsoftBmsApp.Services.RestHelperService.Implementations;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Implementations;
using AirsoftBmsApp.Services.RoomRestService.Implementations;
using AirsoftBmsApp.Services.TeamRestService.Abstractions;
using AirsoftBmsApp.Services.TeamRestService.Implementations;
using AirsoftBmsApp.Validation;
using AirsoftBmsApp.ViewModel.CreateRoomFormViewModel;
using AirsoftBmsApp.ViewModel.JoinRoomFormViewModel;
using AirsoftBmsApp.ViewModel.PlayerFormViewModel;
using AirsoftBmsApp.ViewModel.RoomFormPageViewModel;
using AirsoftBmsApp.ViewModel.RoomViewModel;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;


#if WINDOWS
using Microsoft.UI.Xaml.Controls;
#endif

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

            builder.Services.AddTransient<IRoomViewModel, RoomViewModel>();
            builder.Services.AddTransient<IRoomFormViewModel, RoomFormViewModel>();
            builder.Services.AddTransient<IPlayerFormViewModel, PlayerFormViewModel>();
            builder.Services.AddTransient<ICreateRoomFormViewModel, CreateRoomFormViewModel>();
            builder.Services.AddTransient<IJoinRoomFormViewModel, JoinRoomFormViewModel>();

            bool isUsingMockRestServices = true;

            if (isUsingMockRestServices)
            {
                builder.Services.AddSingleton<IAccountRestService, MockAccountRestService>();
                builder.Services.AddSingleton<IPlayerRestService, MockPlayerRestService>();
                builder.Services.AddSingleton<IRoomRestService, MockRoomRestService>();
                builder.Services.AddSingleton<ITeamRestService, MockTeamRestService>();
            } else
            {
                string baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8080" : "http://localhost:8080";

                builder.Services.AddHttpClient<IAccountRestService, AccountRestService>(client =>
                {
                    client.BaseAddress = new Uri($"{baseAddress}/Account/");
                });

                builder.Services.AddHttpClient<IPlayerRestService, PlayerRestService>(client =>
                {
                    client.BaseAddress = new Uri($"{baseAddress}/Player/");
                });

                builder.Services.AddHttpClient<IRoomRestService, RoomRestService>(client =>
                {
                    client.BaseAddress = new Uri($"{baseAddress}/Room/");
                });

                builder.Services.AddHttpClient<ITeamRestService, TeamRestService>(client =>
                {
                    client.BaseAddress = new Uri($"{baseAddress}/Team/");
                });
            }

            builder.Services.AddSingleton<IValidationHelperFactory, ValidationHelperFactory>();
            builder.Services.AddSingleton<IPlayerDataService, PlayerDataService>();
            builder.Services.AddSingleton<IRoomDataService, RoomDataService>();
            builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
            builder.Services.AddSingleton<IJsonHelperService, JsonHelperService>();

            builder.Services.AddSingleton<IApiFacade, ApiFacade>();
            builder.Services.AddSingleton<IAccountHandler, AccountHandler>();
            builder.Services.AddSingleton<IPlayerHandler, PlayerHandler>();
            builder.Services.AddSingleton<IRoomHandler, RoomHandler>();
            builder.Services.AddSingleton<ITeamHandler, TeamHandler>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

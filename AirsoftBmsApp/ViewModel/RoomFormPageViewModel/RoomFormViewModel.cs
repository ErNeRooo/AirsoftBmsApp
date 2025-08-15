using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.View.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace AirsoftBmsApp.ViewModel.RoomFormPageViewModel
{
    public partial class RoomFormViewModel : ObservableObject, IRoomFormViewModel
    {
        IApiFacade _apiFacade;

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        string errorMessage = "";

        [ObservableProperty]
        string playerName = "";

        public RoomFormViewModel(IPlayerDataService playerDataService, IApiFacade apiFacade)
        {
            _apiFacade = apiFacade;
            PlayerName = string.Format(AppResources.WelcomePlayerHeader, playerDataService.Player.Name);
        }

        [RelayCommand]
        public async Task LogOut()
        {
            IsLoading = true;
            await Task.Yield();

            var result = await _apiFacade.Player.LogOut();

            switch (result)
            {
                case Success:
                    await Redirect("//PlayerFormPage");
                    break;
                case Failure failure:
                    ErrorMessage = failure.errorMessage;
                    break;
                case Error error:
                    ErrorMessage = error.errorMessage;
                    break;
                default:
                    throw new InvalidOperationException("Unknown result type");
            }

            IsLoading = false;
        }

        [RelayCommand]
        async Task OnJoinRoomButtonClicked()
        {
            await Redirect(nameof(JoinRoomPage));
        }

        [RelayCommand]
        async Task OnCreateRoomButtonClicked()
        {
            await Redirect(nameof(CreateRoomPage));
        }

        [RelayCommand]
        async Task Redirect(string path)
        {
#if DEBUG
            var ww = Stopwatch.StartNew();
#endif

            await Shell.Current.GoToAsync(path);

#if DEBUG
            ww.Stop();
            Debug.WriteLine($"Execution took {ww.Elapsed.TotalMilliseconds} ms");
#endif
        }
    }
}

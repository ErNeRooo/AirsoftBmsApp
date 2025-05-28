using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.Handlers.Player;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.ViewModel.RoomFormPageViewModel
{
    public partial class RoomFormViewModel(IPlayerDataService playerDataService, IPlayerRestService playerRestService) : ObservableObject, IRoomFormViewModel
    {
        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        string errorMessage = "";

        [RelayCommand]
        public async Task LogOut()
        {
            IsLoading = true;

            var deletePlayer = new PlayerDeleteHandler(playerRestService, playerDataService);

            var result = await deletePlayer.Handle(playerDataService.Player.Id);

            switch (result)
            {
                case SuccessBase success:
                    await Shell.Current.GoToAsync("//PlayerFormPage");
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
        async Task Redirect(string path)
        {
            await Shell.Current.GoToAsync(path);
        }
    }
}

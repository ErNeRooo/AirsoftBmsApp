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
        [RelayCommand]
        async Task LogOut()
        {
            var deletePlayer = new PlayerDeleteHandler(playerRestService, playerDataService);

            var result = await deletePlayer.Handle(playerDataService.Player.Id);

            switch (result)
            {
                case SuccessBase success:
                    await Shell.Current.GoToAsync("//PlayerFormPage");
                    break;
                default:
                    break;
            }
        }

        [RelayCommand]
        async Task Redirect(string path)
        {
            await Shell.Current.GoToAsync(path);
        }
    }
}

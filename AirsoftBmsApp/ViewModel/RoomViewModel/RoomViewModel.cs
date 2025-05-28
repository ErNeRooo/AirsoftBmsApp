using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AirsoftBmsApp.Model;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.Handlers.Player;
using AirsoftBmsApp.Networking.Handlers.Room;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AirsoftBmsApp.ViewModel.RoomViewModel
{
    public partial class RoomViewModel(IPlayerDataService playerDataService, IPlayerRestService playerRestService, IRoomRestService roomRestService, IRoomDataService roomDataService) : ObservableObject, IRoomViewModel
    {
        [ObservableProperty]
        ObservableRoom room = roomDataService.Room;

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        string errorMessage = "";

        [RelayCommand]
        public async Task LeaveRoom()
        {
            IsLoading = true;

            var leaveRoom = new RoomLeaveHandler(roomRestService, roomDataService, playerDataService);

            var result = await leaveRoom.Handle(null);

            switch (result)
            {
                case SuccessBase _:
                    await Shell.Current.GoToAsync(nameof(RoomFormPage));
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
    }
}

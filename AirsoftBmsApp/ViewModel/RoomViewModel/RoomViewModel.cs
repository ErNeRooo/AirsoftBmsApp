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
        [RelayCommand]
        async Task LeaveRoom()
        {
            var leaveRoom = new RoomLeaveHandler(roomRestService, roomDataService, playerDataService);

            var result = await leaveRoom.Handle(null);

            switch (result)
            {
                case SuccessBase _:
                    await Shell.Current.GoToAsync(nameof(RoomFormPage));
                    break;
            }
        }
    }
}

using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.Handlers.Player;
using AirsoftBmsApp.Networking.Handlers.Room;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Validation;
using AirsoftBmsApp.Validation.Rules;
using AirsoftBmsApp.View.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AirsoftBmsApp.ViewModel.CreateRoomFormViewModel
{
    public partial class CreateRoomFormViewModel : ObservableObject, ICreateRoomFormViewModel
    {
        IPlayerDataService _playerDataService;
        IRoomDataService _roomDataService;
        IRoomRestService _roomRestService;

        [ObservableProperty]
        RoomForm roomForm = new();

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        string errorMessage = "";

        public CreateRoomFormViewModel(IValidationHelperFactory validationHelperFactory, IPlayerDataService playerDataService, IRoomDataService roomDataService, IRoomRestService roomRestService)
        {
            validationHelperFactory.AddValidations(roomForm);
            _playerDataService = playerDataService;
            _roomDataService = roomDataService;
            _roomRestService = roomRestService;
        }

        [RelayCommand]
        void Validate()
        {
            roomForm.JoinCode.Validate();
            roomForm.Password.Validate();
        }

        [RelayCommand]
        void ValidateJoinCode()
        {
            roomForm.JoinCode.Validate();
        }

        [RelayCommand]
        void ValidatePassword()
        {
            roomForm.Password.Validate();
        }

        [RelayCommand]
        async Task CreateRoomAsync()
        {
            Validate();
            if (!roomForm.JoinCode.IsValid || !roomForm.Password.IsValid) return;

            IsLoading = true;

            var handler = new RoomPostHandler(_roomRestService, _roomDataService, _playerDataService);

            var postRoomDto = new PostRoomDto
            {
                JoinCode = roomForm.JoinCode.Value,
                Password = roomForm.Password.Value,
                MaxPlayers = 50,
            };

            var result = await handler.Handle(postRoomDto);

            switch (result)
            {
                case SuccessBase success:
                    await Shell.Current.GoToAsync(nameof(RoomMembersPage));
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

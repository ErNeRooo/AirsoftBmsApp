using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.Handlers.Room;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Validation;
using AirsoftBmsApp.Validation.Rules;
using AirsoftBmsApp.View.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AirsoftBmsApp.ViewModel.JoinRoomFormViewModel
{
    public partial class JoinRoomFormViewModel : ObservableObject, IJoinRoomFormViewModel
    {
        IPlayerDataService _playerDataService;
        IRoomDataService _roomDataService;
        IRoomRestService _roomRestService;

        [ObservableProperty]
        ValidatableJoinRoomForm roomForm = new();

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        string errorMessage = "";

        public JoinRoomFormViewModel(IValidationHelperFactory validationHelperFactory, IPlayerDataService playerDataService, IRoomDataService roomDataService, IRoomRestService roomRestService)
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
        async Task Redirect(string path)
        {
            await Shell.Current.GoToAsync(path);
        }

        [RelayCommand]
        async void JoinRoomAsync()
        {
            Validate();
            if (!roomForm.JoinCode.IsValid || !roomForm.Password.IsValid) return;

            IsLoading = true;

            var handler = new RoomJoinHandler(_roomRestService, _roomDataService, _playerDataService);

            var joinRoomDto = new JoinRoomDto
            {
                JoinCode = roomForm.JoinCode.Value,
                Password = roomForm.Password.Value
            };

            var result = await handler.Handle(joinRoomDto);

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

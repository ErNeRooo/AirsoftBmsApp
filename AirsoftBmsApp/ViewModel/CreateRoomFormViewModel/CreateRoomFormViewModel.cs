using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Validation;
using AirsoftBmsApp.View.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AirsoftBmsApp.ViewModel.CreateRoomFormViewModel
{
    public partial class CreateRoomFormViewModel : ObservableObject, ICreateRoomFormViewModel
    {
        IApiFacade _apiFacade;

        [ObservableProperty]
        ValidatableCreateRoomForm roomForm = new();

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        string errorMessage = "";

        public CreateRoomFormViewModel(
            IValidationHelperFactory validationHelperFactory, 
            IApiFacade apiFacade
            )
        {
            validationHelperFactory.AddValidations(roomForm);
            _apiFacade = apiFacade;
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
        async Task CreateRoomAsync()
        {
            Validate();
            if (!roomForm.JoinCode.IsValid || !roomForm.Password.IsValid) return;

            IsLoading = true;

            var postRoomDto = new PostRoomDto
            {
                JoinCode = roomForm.JoinCode.Value,
                Password = roomForm.Password.Value,
                MaxPlayers = 50,
            };

            HttpResult result = await _apiFacade.Room.Create(postRoomDto);

            switch (result)
            {
                case Success:
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

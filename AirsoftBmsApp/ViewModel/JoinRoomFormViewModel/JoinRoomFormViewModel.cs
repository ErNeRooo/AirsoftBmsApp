﻿using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Validation;
using AirsoftBmsApp.View.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AirsoftBmsApp.ViewModel.JoinRoomFormViewModel
{
    public partial class JoinRoomFormViewModel : ObservableObject, IJoinRoomFormViewModel
    {
        IApiFacade _apiFacade;

        [ObservableProperty]
        ValidatableJoinRoomForm roomForm = new();

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        string errorMessage = "";

        public JoinRoomFormViewModel(IValidationHelperFactory validationHelperFactory, IApiFacade apiFacade)
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
        async void JoinRoomAsync()
        {
            Validate();
            if (!roomForm.JoinCode.IsValid || !roomForm.Password.IsValid) return;

            IsLoading = true;

            var joinRoomDto = new JoinRoomDto
            {
                JoinCode = roomForm.JoinCode.Value,
                Password = roomForm.Password.Value
            };

            HttpResult result = await _apiFacade.Room.Join(joinRoomDto);

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

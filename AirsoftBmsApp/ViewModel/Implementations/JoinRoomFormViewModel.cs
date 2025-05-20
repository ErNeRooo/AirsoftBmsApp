using AirsoftBmsApp.Model;
using AirsoftBmsApp.Validation.Rules;
using AirsoftBmsApp.View.Pages;
using AirsoftBmsApp.ViewModel.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.ViewModel
{
    public partial class JoinRoomFormViewModel : ObservableObject, IJoinRoomFormViewModel
    {
        [ObservableProperty]
        RoomForm roomForm = new();

        public JoinRoomFormViewModel()
        {
            roomForm.JoinCode.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Join code is required."
            });
            roomForm.JoinCode.Validations.Add(new HasLengthRule<string>
            {
                ValidationMessage = "Join code must be 6 characters.",
                Length = 6
            });
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
        async void JoinRoomAsync()
        {
            Validate();
            if (!roomForm.JoinCode.IsValid || !roomForm.Password.IsValid) return;

            await Shell.Current.GoToAsync(nameof(RoomMembersPage));
        }
    }
}

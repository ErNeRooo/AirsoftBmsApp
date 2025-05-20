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
    public partial class CreateRoomFormViewModel : ObservableObject, ICreateRoomFormViewModel
    {
        [ObservableProperty]
        RoomForm roomForm = new();

        public CreateRoomFormViewModel()
        {
            roomForm.JoinCode.Validations.Add(new OptionalLengthRule<string>
            {
                ValidationMessage = "If join code is specified it must be 6 characters.",
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
        async void CreateRoomAsync()
        {
            Validate();
            if (!roomForm.JoinCode.IsValid || !roomForm.Password.IsValid) return;

            await Shell.Current.GoToAsync(nameof(RoomMembersPage));
        }
    }
}

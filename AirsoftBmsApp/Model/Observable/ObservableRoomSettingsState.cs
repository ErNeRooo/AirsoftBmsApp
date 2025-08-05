using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Validation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Observable
{
    public partial class ObservableRoomSettingsState : ObservableObject
    {
        [ObservableProperty]
        bool isVisible = false;

        [ObservableProperty]
        ValidatableUpdateRoomForm roomForm = new();

        [ObservableProperty]
        ObservablePlayer? selectedPlayerToBecomeAdmin;

        [ObservableProperty]
        ObservableCollection<ObservablePlayer> players = new();

        public ObservableRoomSettingsState(IValidationHelperFactory validationHelperFactory)
        {
            validationHelperFactory.AddValidations(RoomForm);
        }

        partial void OnPlayersChanged(ObservableCollection<ObservablePlayer> value)
        {
            ObservablePlayer? adminPlayer = value.FirstOrDefault(p => p.IsAdmin);

            SelectedPlayerToBecomeAdmin = adminPlayer;
        }

        [RelayCommand]
        public void ValidateJoinCode() => RoomForm.JoinCode.Validate();

        [RelayCommand]
        public void ValidateMaxPlayers() => RoomForm.MaxPlayers.Validate();

        [RelayCommand]
        public void ValidatePassword() => RoomForm.Password.Validate();
    }
}

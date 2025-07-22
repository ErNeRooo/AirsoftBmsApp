using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Validation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Model.Observable
{
    public partial class ObservableTeamSettingsState : ObservableObject
    {
        [ObservableProperty]
        ObservableTeam? team;

        [ObservableProperty]
        bool isVisible;

        [ObservableProperty]
        ObservableCollection<ObservablePlayer> players = new();

        [ObservableProperty]
        ValidatableTeamForm teamForm = new();

        [ObservableProperty]
        ObservablePlayer? selectedPlayerToBecomeOfficer;

        public ObservableTeamSettingsState(IValidationHelperFactory validationHelperFactory)
        {
            validationHelperFactory.AddValidations(TeamForm);
        }

        partial void OnPlayersChanged(ObservableCollection<ObservablePlayer> value)
        {
            ObservablePlayer? officerPlayer = value.FirstOrDefault(p => p.IsOfficer);

            SelectedPlayerToBecomeOfficer = officerPlayer;
        }

        [RelayCommand]
        public void Validate()
        {
            TeamForm.Name.Validate();
        }
    }
}

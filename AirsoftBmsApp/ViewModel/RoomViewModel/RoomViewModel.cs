using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Validation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Room;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.ViewModel.RoomViewModel
{
    public partial class RoomViewModel : ObservableObject, IRoomViewModel
    {
        IApiFacade _apiFacade;

        [ObservableProperty]
        ObservablePlayer player;

        [ObservableProperty]
        ObservableRoom room;

        [ObservableProperty]
        ValidatableTeamForm teamForm = new ValidatableTeamForm();

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        bool isCreateTeamDialogVisible = false;

        [ObservableProperty]
        string errorMessage = "";

        [ObservableProperty]
        ObservableConfirmationDialogState confirmationDialogState = new();

        [ObservableProperty]
        string informationDialogMessage = "";

        [ObservableProperty]
        ObservableTeamSettingsState teamSettingsState;

        [ObservableProperty]
        ObservableRoomSettingsState roomSettingsState;

        [ObservableProperty]
        int targetTeamId = 0;

        [ObservableProperty]
        bool isOfficerOrAdmin = true;

        [ObservableProperty]
        bool isAdmin = false;

        public RoomViewModel(
            IValidationHelperFactory validationHelperFactory,
            IRoomDataService roomDataService,
            IApiFacade apiFacade,
            IPlayerDataService playerDataService
            )
        {
            _apiFacade = apiFacade;
            Player = playerDataService.Player;

            Room = roomDataService.Room;

            TeamSettingsState = new ObservableTeamSettingsState(validationHelperFactory);
            RoomSettingsState = new ObservableRoomSettingsState(validationHelperFactory);

            validationHelperFactory.AddValidations(TeamForm);
        }

        [RelayCommand]
        public async Task CreateTeam()
        {
            ValidateName();
            if (!TeamForm.Name.IsValid) return;

            IsLoading = true;

            PostTeamDto postTeamDto = new PostTeamDto
            {
                Name = TeamForm.Name.Value,
                RoomId = Room.Id
            };

            var result = await _apiFacade.Team.Create(postTeamDto);

            switch (result)
            {
                case Success:
                    IsCreateTeamDialogVisible = false;

                    TeamForm.Name.Value = "";
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

            TeamForm = new ValidatableTeamForm();

            IsLoading = false;
        }
        
        [RelayCommand]
        public async Task SwitchTeamConfirmation(int teamId)
        {
            if (Player.TeamId == teamId) return;

            TargetTeamId = teamId;

            string teamName = Room.Teams.FirstOrDefault(t => t.Id == teamId)?.Name ?? "Unknown Team";

            ConfirmationDialogState.Message = teamId == 0
                ? "Are you sure you want to leave your team?"
                : $"Are you sure you want to switch to team {teamName}?";
            ConfirmationDialogState.Command = SwitchTeamCommand;
        }

        [RelayCommand]
        public async Task SwitchTeam()
        {
            ConfirmationDialogState.Message = "";
            IsLoading = true;

            if (TargetTeamId == 0) 
            { 
                LeaveTeam();
                IsLoading = false;
                return;            
            }

            var playerWithSwitchedTeam = new PutPlayerDto
            {
                TeamId = TargetTeamId
            };

            var result = await _apiFacade.Player.Update(playerWithSwitchedTeam);

            HandleFailures(result);

            IsLoading = false;
        }

        [RelayCommand]
        public async Task LeaveTeam()
        {
            IsLoading = true;

            var result = await _apiFacade.Team.Leave();

            HandleFailures(result);

            IsLoading = false;
        }

        [RelayCommand]
        public async Task ValidateName()
        {
            TeamForm.Name.Validate();
        }

        [RelayCommand]
        public async Task CreateTeamButtonClicked()
        {
            IsCreateTeamDialogVisible = true;
        }

        [RelayCommand]
        public async Task CancelTeamDialogClicked()
        {
            IsCreateTeamDialogVisible = false;
        }

        [RelayCommand]
        public async Task LeaveRoomConfirmation()
        {
            ConfirmationDialogState.Message = "Are you sure you want to leave the room?";
            ConfirmationDialogState.Command = LeaveRoomCommand;
        }

        [RelayCommand]
        public async Task LeaveRoom()
        {
            IsLoading = true;

            var result = await _apiFacade.Room.Leave();

            switch (result)
            {
                case Success:
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

        [RelayCommand]
        public async Task TakeAdminConfirmation()
        {
            ConfirmationDialogState.Message = "Are you sure you want to take admin role?";
            ConfirmationDialogState.Command = TakeAdminCommand;
        }

        [RelayCommand]
        public async Task TakeAdmin()
        {
            ConfirmationDialogState.Message = "";
            IsLoading = true;

            PutRoomDto roomDto = new PutRoomDto
            {
                AdminPlayerId = Player.Id
            };

            var result = await _apiFacade.Room.Update(roomDto);

            switch (result)
            {
                case Success:
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

        [RelayCommand]
        public async Task TakeOfficerConfirmation(int teamId)
        {
            if(teamId != Player.TeamId)
            {
                InformationDialogMessage = "You can only take officer role in your own team.";
                return;
            }

            ConfirmationDialogState.Message = "Are you sure you want to take officer role?";
            ConfirmationDialogState.Command = TakeOfficerCommand;
        }

        [RelayCommand]
        public async Task TakeOfficer()
        {
            ConfirmationDialogState.Message = "";

            if (Player.TeamId is null) return;

            IsLoading = true;

            PutTeamDto teamDto = new()
            {
                OfficerPlayerId = Player.Id
            };

            var result = await _apiFacade.Team.Update(teamDto, (int)Player.TeamId);

            switch (result)
            {
                case Success:
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

        [RelayCommand]
        public async Task ShowTeamSettings(int teamId)
        {
            TargetTeamId = teamId;
            var team = Room.Teams.FirstOrDefault(t => t.Id == teamId);

            if (team == null) return;

            TeamSettingsState.Team = team;
            TeamSettingsState.Players = team.Players;
            TeamSettingsState.IsVisible = true;
        }

        [RelayCommand]
        public async Task ShowRoomSettings()
        {
            RoomSettingsState.Players = new ObservableCollection<ObservablePlayer>();

            foreach (var team in Room.Teams)
            {
                foreach(var player in team.Players)
                {
                    RoomSettingsState.Players.Add(player);
                }
            }

            RoomSettingsState.IsVisible = true;
        }

        [RelayCommand]
        public async Task UpdateTeam()
        {
            if (!TeamSettingsState.TeamForm.Name.IsValid) return;

            IsLoading = true;

            PutTeamDto teamDto = new()
            {
                Name = TeamSettingsState.TeamForm.Name.Value,
                OfficerPlayerId = TeamSettingsState.SelectedPlayerToBecomeOfficer?.Id,
            };

            var result = await _apiFacade.Team.Update(teamDto, TargetTeamId);

            TeamSettingsState.IsVisible = false;

            switch (result)
            {
                case Success:
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

        [RelayCommand]
        public async Task DeleteTeamConfirmation()
        {
            string teamName = Room.Teams.FirstOrDefault(t => t.Id == TargetTeamId)?.Name ?? "Unknown Team";

            ConfirmationDialogState.Message = $"Are you sure you want to delete team {teamName}?";
            ConfirmationDialogState.Command = DeleteTeamCommand;
        }

        [RelayCommand]
        public async Task DeleteTeam()
        {
            ConfirmationDialogState.Message = "";
            TeamSettingsState.IsVisible = false;

            IsLoading = true;

            var result = await _apiFacade.Team.Delete(TargetTeamId);

            switch (result)
            {
                case Success:
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

        [RelayCommand]
        public async Task DeleteRoomConfirmation()
        {
            ConfirmationDialogState.Message = $"Are you sure you want to delete the room?";
            ConfirmationDialogState.Command = DeleteRoomCommand;
        }

        [RelayCommand]
        public async Task DeleteRoom()
        {
            ConfirmationDialogState.Message = "";
            TeamSettingsState.IsVisible = false;

            IsLoading = true;

            var result = await _apiFacade.Room.Delete();

            switch (result)
            {
                case Success:
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

        [RelayCommand]
        public async Task UpdateRoom()
        {
            if (!RoomSettingsState.RoomForm.JoinCode.IsValid) return;
            if (!RoomSettingsState.RoomForm.Password.IsValid) return;
            if (!RoomSettingsState.RoomForm.MaxPlayers.IsValid) return;

            IsLoading = true;

            PutRoomDto roomDto = new()
            {
                MaxPlayers = RoomSettingsState.RoomForm.MaxPlayers.Value,
                JoinCode = RoomSettingsState.RoomForm.JoinCode.Value,
                Password = RoomSettingsState.RoomForm.Password.Value,
                AdminPlayerId = RoomSettingsState.SelectedPlayerToBecomeAdmin is null 
                    ? null
                    : RoomSettingsState.SelectedPlayerToBecomeAdmin.Id
            };

            var result = await _apiFacade.Room.Update(roomDto);

            RoomSettingsState.IsVisible = false;

            switch (result)
            {
                case Success:
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

        private void HandleFailures(HttpResult result)
        {
            switch (result)
            {
                case Success success:
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
        }
    }
}

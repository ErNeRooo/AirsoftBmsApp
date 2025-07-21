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

namespace AirsoftBmsApp.ViewModel.RoomViewModel
{
    public partial class RoomViewModel : ObservableObject, IRoomViewModel
    {
        IApiFacade _apiFacade;
        IPlayerDataService _playerDataService;

        [ObservableProperty]
        ObservableRoom room;

        [ObservableProperty]
        ValidatableTeamForm teamForm = new ValidatableTeamForm();

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        bool isCreateTeamButtonVisible;

        [ObservableProperty]
        bool isCreateTeamDialogVisible = false;

        [ObservableProperty]
        string errorMessage = "";

        [ObservableProperty]
        ObservableConfirmationDialogState confirmationDialogState = new()
        {
            Message = "",
            Command = null
        };

        [ObservableProperty]
        string informationDialogMessage = "";

        int TargetTeamId = 0;

        public RoomViewModel(
            IValidationHelperFactory validationHelperFactory,
            IRoomDataService roomDataService,
            IApiFacade apiFacade,
            IPlayerDataService playerDataService
            )
        {
            _apiFacade = apiFacade;
            _playerDataService = playerDataService;

            Room = roomDataService.Room;
            IsCreateTeamButtonVisible = roomDataService.Room.AdminPlayerId == _playerDataService.Player.Id;

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
                Name = teamForm.Name.Value,
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
                AdminPlayerId = _playerDataService.Player.Id
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
            if(teamId != _playerDataService.Player.TeamId)
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

            if (_playerDataService.Player.TeamId is null) return;

            IsLoading = true;

            PutTeamDto teamDto = new()
            {
                OfficerPlayerId = _playerDataService.Player.Id
            };

            var result = await _apiFacade.Team.Update(teamDto, (int)_playerDataService.Player.TeamId);

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

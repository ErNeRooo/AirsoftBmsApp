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
        public async Task SwitchTeam(int teamId)
        {
            if (teamId == 0) 
            { 
                LeaveTeam();
                return;            
            }

            IsLoading = true;

            var playerWithSwitchedTeam = new PutPlayerDto
            {
                TeamId = teamId
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

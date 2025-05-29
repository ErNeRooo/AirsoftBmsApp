using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Networking.Handlers.Room;
using AirsoftBmsApp.Networking.Handlers.Team;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.TeamRestService.Abstractions;
using AirsoftBmsApp.Validation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AirsoftBmsApp.ViewModel.RoomViewModel
{
    public partial class RoomViewModel : ObservableObject, IRoomViewModel
    {
        IPlayerDataService _playerDataService;
        IRoomDataService _roomDataService;
        IRoomRestService _roomRestService;
        ITeamRestService _teamRestService;

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
            IPlayerDataService playerDataService,
            IRoomRestService roomRestService,
            IRoomDataService roomDataService,
            ITeamRestService teamRestService
            )
        {
            _playerDataService = playerDataService;
            _roomRestService = roomRestService;
            _roomDataService = roomDataService;
            _teamRestService = teamRestService;

            room = _roomDataService.Room;
            isCreateTeamButtonVisible = _roomDataService.Room.AdminPlayerId == _playerDataService.Player.Id;

            validationHelperFactory.AddValidations(TeamForm);
        }

        [RelayCommand]
        public async Task CreateTeam()
        {
            ValidateName();
            if (!TeamForm.Name.IsValid) return;

            IsLoading = true;

            var createRoom = new TeamPostHandler(_teamRestService, _roomDataService);

            PostTeamDto postRoomDto = new PostTeamDto
            {
                Name = teamForm.Name.Value,
            };

            var result = await createRoom.Handle(postRoomDto);

            switch (result)
            {
                case SuccessBase _:
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

            var leaveRoom = new RoomLeaveHandler(_roomRestService, _roomDataService, _playerDataService);

            var result = await leaveRoom.Handle(null);

            switch (result)
            {
                case SuccessBase _:
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
    }
}

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
using AirsoftBmsApp.Resources.Languages;
using System.Diagnostics;
using AirsoftBmsApp.View.Pages;
using MethodTimer;
using AirsoftBmsApp.Services.HubConnectionService;
using Microsoft.AspNetCore.SignalR.Client;
using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Services.HubNotificationHandlerService;
using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Services.GeolocationService;

namespace AirsoftBmsApp.ViewModel.RoomViewModel
{
    public partial class RoomViewModel : ObservableObject, IRoomViewModel
    {
        private readonly IApiFacade _apiFacade;
        private readonly IHubConnectionService _hubConnectionService;
        private readonly IRoomDataService _roomDataService;
        private readonly IPlayerDataService _playerDataService; 

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
        ObservablePlayerProfileState playerProfileState;

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
            IPlayerDataService playerDataService,
            IHubConnectionService hubConnectionService,
            IHubNotificationHandlerService notificationHandlers, 
            IGeolocationService geolocationService
            )
        {
            _hubConnectionService = hubConnectionService;
            _apiFacade = apiFacade;
            _roomDataService = roomDataService;
            _playerDataService = playerDataService;

            Player = playerDataService.Player;
            playerDataService.PlayerChanged += (_, newPlayer) => Player = newPlayer;
            Room = roomDataService.Room;
            roomDataService.RoomChanged += (_, newRoom) => Room = newRoom;

            TeamSettingsState = new ObservableTeamSettingsState(validationHelperFactory);
            RoomSettingsState = new ObservableRoomSettingsState(validationHelperFactory, Room.MaxPlayers);
            PlayerProfileState = new ObservablePlayerProfileState();
            PlayerProfileState.SelfPlayer = Player;

            validationHelperFactory.AddValidations(TeamForm);

            SetNotificationHandlers(notificationHandlers, hubConnectionService, roomDataService, playerDataService, geolocationService);
        }

        void SetNotificationHandlers(
            IHubNotificationHandlerService notificationHandlers,
            IHubConnectionService hubConnectionService,
            IRoomDataService roomDataService,
            IPlayerDataService playerDataService, 
            IGeolocationService geolocationService
            )
        {
            _hubConnectionService.HubConnection.On<DeathDto>(
                HubNotifications.DeathCreated,
                (deathDto) => notificationHandlers.Death.OnDeathCreated(deathDto, Room));

            _hubConnectionService.HubConnection.On<int>(
                HubNotifications.DeathDeleted, 
                (deathId) => notificationHandlers.Death.OnDeathDeleted(deathId, Room));


            _hubConnectionService.HubConnection.On<KillDto>(
                HubNotifications.KillCreated,
                (killDto) => notificationHandlers.Kill.OnKillCreated(killDto, Room));

            _hubConnectionService.HubConnection.On<int>(
                HubNotifications.KillDeleted,
                (killId) => notificationHandlers.Kill.OnKillDeleted(killId, Room));


            _hubConnectionService.HubConnection.On<PlayerDto>(
                HubNotifications.PlayerUpdated,
                (playerDto) => notificationHandlers.Player.OnPlayerUpdated(playerDto, Room));

            _hubConnectionService.HubConnection.On<int>(
                HubNotifications.PlayerLeftTeam,
                (playerId) => notificationHandlers.Player.OnPlayerLeftTeam(playerId, Room));

            _hubConnectionService.HubConnection.On<int>(
                HubNotifications.PlayerLeftRoom,
                (playerId) => notificationHandlers.Player.OnPlayerLeftRoom(playerId, roomDataService, playerDataService, hubConnectionService, geolocationService));


            _hubConnectionService.HubConnection.On<PlayerDto>(
                HubNotifications.RoomJoined,
                (playerDto) => notificationHandlers.Room.OnRoomJoined(playerDto, Room));

            _hubConnectionService.HubConnection.On<RoomDto>(
                HubNotifications.RoomUpdated,
                (roomDto) => notificationHandlers.Room.OnRoomUpdated(roomDto, Room));

            _hubConnectionService.HubConnection.On(
                HubNotifications.RoomDeleted, 
                () =>
                {
                    notificationHandlers.Room.OnRoomDeleted(roomDataService, playerDataService, hubConnectionService);
                });


            _hubConnectionService.HubConnection.On<TeamDto>(
                HubNotifications.TeamCreated,
                (playerDto) => notificationHandlers.Team.OnTeamCreated(playerDto, Room));

            _hubConnectionService.HubConnection.On<TeamDto>(
                HubNotifications.TeamUpdated,
                (teamDto) => notificationHandlers.Team.OnTeamUpdated(teamDto, Room));

            _hubConnectionService.HubConnection.On<int>(
                HubNotifications.TeamDeleted,
                (teamId) => notificationHandlers.Team.OnTeamDeleted(teamId, Room));
        }

        [RelayCommand]
        async Task Redirect(string path)
        {
#if DEBUG
            var ww = Stopwatch.StartNew();
#endif

            await Shell.Current.GoToAsync(path, animate: false);

#if DEBUG
            ww.Stop();
            Debug.WriteLine($"Execution took {ww.Elapsed.TotalMilliseconds} ms");
#endif
        }

        [RelayCommand]
        public async Task CreateTeam()
        {
            ValidateName();
            if (!TeamForm.Name.IsValid) return;

            IsLoading = true;
            await Task.Yield();

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

        [Time]
        [RelayCommand]
        public async Task SwitchTeamConfirmation(int teamId)
        {
            if(Room.Battle is not null && Room.Battle.IsActive) 
            {
                InformationDialogMessage = AppResources.CannotSwitchTeamDuringABattleInformationMessage;
                return;
            }

            if (Player.TeamId == teamId) return;

            TargetTeamId = teamId;

            string teamName = Room.Teams.FirstOrDefault(t => t.Id == teamId)?.Name ?? "Unknown Team";

            ConfirmationDialogState.Message = teamId == 0
                ? AppResources.LeaveTeamConfirmationMessage
                : string.Format(AppResources.SwitchTeamConfirmationMessage, teamName);
            ConfirmationDialogState.Command = SwitchTeamCommand;
        }

        [Time]
        [RelayCommand]
        public async Task SwitchTeam()
        {
            ConfirmationDialogState.Message = "";
            IsLoading = true;
            await Task.Yield();

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

            var result = await _apiFacade.Player.Update(playerWithSwitchedTeam, Player.Id);

            HandleFailures(result);

            IsLoading = false;
        }

        [Time]
        [RelayCommand]
        public async Task LeaveTeam()
        {
            IsLoading = true;
            await Task.Yield();

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
            ConfirmationDialogState.Message = AppResources.LeaveTeamConfirmationMessage;
            ConfirmationDialogState.Command = LeaveRoomCommand;
        }

        [RelayCommand]
        public async Task LeaveRoom()
        {
            IsLoading = true;
            await Task.Yield();

            var result = await _apiFacade.Room.Leave();

            switch (result)
            {
                case Success:
                    _roomDataService.Room = new();
                    _playerDataService.Player = new() { Id = Player.Id, Name = Player.Name };

                    ConfirmationDialogState.Message = "";
                    ConfirmationDialogState.Command = null;

                    await _hubConnectionService.StopConnection();
                    await Redirect("../..");
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
            ConfirmationDialogState.Message = AppResources.TakeAdminRoleConfirmationMessage;
            ConfirmationDialogState.Command = TakeAdminCommand;
        }

        [RelayCommand]
        public async Task TakeAdmin()
        {
            ConfirmationDialogState.Message = "";
            IsLoading = true;
            await Task.Yield();

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
                InformationDialogMessage = AppResources.YouCanOnlyTakeOfficerInYourTeamInformationMessage;
                return;
            }

            ConfirmationDialogState.Message = AppResources.TakeOfficerConfirmationMessage;
            ConfirmationDialogState.Command = TakeOfficerCommand;
        }

        [RelayCommand]
        public async Task TakeOfficer()
        {
            ConfirmationDialogState.Message = "";

            if (Player.TeamId is null) return;

            IsLoading = true;
            await Task.Yield();

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
            var newPlayers = new ObservableCollection<ObservablePlayer>();

            foreach (var team in Room.Teams)
            {
                foreach(var player in team.Players)
                {
                    newPlayers.Add(player);
                }
            }

            RoomSettingsState.Players = newPlayers;
            RoomSettingsState.IsVisible = true;
        }

        [RelayCommand]
        public async Task ShowPlayerProfileSettings(int playerId)
        {
            ObservablePlayer? targetPlayer = Room.Teams.SelectMany(team => team.Players).FirstOrDefault(player => player.Id == playerId);

            if(targetPlayer is not null)
            {
                targetPlayer.TeamId ??= 0;
                PlayerProfileState.SelectedPlayer = targetPlayer;
                PlayerProfileState.Teams = new(Room.Teams.Skip(1));

                PlayerProfileState.IsVisible = true;
            }
            else
            {
                InformationDialogMessage = AppResources.PlayerDoesntExistInformationMessage;
            }
        }

        [RelayCommand]
        public async Task UpdateTeam()
        {
            if (!TeamSettingsState.TeamForm.Name.IsValid) return;

            IsLoading = true;
            await Task.Yield();

            PutTeamDto teamDto = new()
            {
                Name = TeamSettingsState.TeamForm.Name.Value,
                OfficerPlayerId = TeamSettingsState.SelectedPlayerToBecomeOfficer?.Id,
            };

            var result = await _apiFacade.Team.Update(teamDto, TargetTeamId);

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

            TeamSettingsState.IsVisible = false;
            IsLoading = false;
        }

        [RelayCommand]
        public async Task DeleteTeamConfirmation()
        {
            string teamName = Room.Teams.FirstOrDefault(t => t.Id == TargetTeamId)?.Name ?? "Unknown Team";

            ConfirmationDialogState.Message = string.Format(AppResources.DeleteTeamConfirmationMessage, teamName);
            ConfirmationDialogState.Command = DeleteTeamCommand;
        }

        [RelayCommand]
        public async Task DeleteTeam()
        {
            ConfirmationDialogState.Message = "";
            TeamSettingsState.IsVisible = false;

            IsLoading = true;
            await Task.Yield();

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
            ConfirmationDialogState.Message = AppResources.DeleteRoomConfirmationMessage;
            ConfirmationDialogState.Command = DeleteRoomCommand;
        }

        [RelayCommand]
        public async Task DeleteRoom()
        {
            ConfirmationDialogState.Message = "";
            TeamSettingsState.IsVisible = false;

            IsLoading = true;
            await Task.Yield();

            var result = await _apiFacade.Room.Delete();

            switch (result)
            {
                case Success:
                    await Redirect(nameof(RoomFormPage));
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
            await Task.Yield();

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

            RoomSettingsState.IsVisible = false;
            IsLoading = false;
        }

        [Time]
        [RelayCommand]
        public async Task KickFromRoom()
        {
            IsLoading = true;
            await Task.Yield();

            var result = await _apiFacade.Player.KickFromRoom(PlayerProfileState.SelectedPlayer.Id);

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

            PlayerProfileState.IsVisible = false;
            IsLoading = false;
        }

        [Time]
        [RelayCommand]
        public async Task KickFromTeam()
        {
            IsLoading = true;
            await Task.Yield();

            var result = await _apiFacade.Player.KickFromTeam(PlayerProfileState.SelectedPlayer.Id);

            RoomSettingsState.IsVisible = false;

            switch (result)
            {
                case Success:
                    PlayerProfileState.IsVisible = false;
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

        [Time]
        [RelayCommand]
        public async Task MovePlayerToAnotherTeam(ObservableTeam team)
        {
            int currentTeamId = PlayerProfileState.SelectedPlayer.TeamId ?? 0;
            if (team is null || team.Id == currentTeamId) return;

            IsLoading = true;
            await Task.Yield();

            PutPlayerDto playerDto = new()
            {
                TeamId = team.Id
            };

            var result = await _apiFacade.Player.Update(playerDto, PlayerProfileState.SelectedPlayer.Id);

            RoomSettingsState.IsVisible = false;
            PlayerProfileState.IsVisible = false;

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

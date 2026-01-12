using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.GeolocationService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Utils;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Room;

public class RoomHandler(
    IRoomRestService roomRestService,
    IRoomDataService roomDataService,
    IPlayerDataService playerDataService,
    IGeolocationService geolocationService
    ) : IRoomHandler
{
    public async Task<HttpResult> Create(PostRoomDto postRoomDto)
    {
        try
        {
            (HttpResult result, RoomDto? room) = await roomRestService.PostAsync(postRoomDto);

            if (result is Success) { 
                roomDataService.Room = new ObservableRoom(room);
                roomDataService.Room.Teams[0].Players.Add(playerDataService.Player);

                playerDataService.Player.RoomId = room.RoomId;
                playerDataService.Player.IsAdmin = true;
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> Join(JoinRoomDto joinRoomDto)
    {
        try
        {
            (HttpResult joinResult, RoomIncludingRelatedEntitiesDto? room) = await roomRestService.JoinAsync(joinRoomDto);

            if (joinResult is Success && room is not null)
            {
                playerDataService.Player.RoomId = room.RoomId;

                Action OnBattleActivated = async () => await geolocationService.Start();
                Action OnBattleDeactivated = geolocationService.Stop;

                var observableRoom = new ObservableRoom(room, OnBattleActivated, OnBattleDeactivated);

                roomDataService.Room = observableRoom;

                InjectObservablePlayerObjectFromPlayerDataService(roomDataService.Room);
            }
            else if (joinResult is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return joinResult;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    private void InjectObservablePlayerObjectFromPlayerDataService(ObservableRoom room)
    {
        ObservablePlayer oldPlayerObject = room.Teams.FindPlayerWithId(playerDataService.Player.Id);

        if (oldPlayerObject is null) return;

        playerDataService.Player.IsAdmin = oldPlayerObject.IsAdmin;
        room.Detach(oldPlayerObject);
        room.Teams.ReplacePlayerWithId(playerDataService.Player.Id, playerDataService.Player);
        room.Attach(playerDataService.Player);
    }

    public async Task<HttpResult> Leave()
    {
        try
        {
            HttpResult result = await roomRestService.LeaveAsync();

            if (result is Success) {
                geolocationService.Stop();
                roomDataService.Room = new ObservableRoom();
                playerDataService.Player.IsAdmin = false;
                playerDataService.Player.IsOfficer = false;
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> Update(PutRoomDto putRoomDto)
    {
        try
        {
            (HttpResult result, RoomDto? room) = await roomRestService.PutAsync(putRoomDto);

            if (result is Success)
            {
                if (roomDataService.Room.AdminPlayerId != room?.AdminPlayerId)
                {
                    ObservablePlayer? oldAdminPlayer = roomDataService.Room.Teams.SelectMany(t => t.Players).FirstOrDefault(p => p.Id == roomDataService.Room.AdminPlayerId);
                    ObservablePlayer? newAdminPlayer = roomDataService.Room.Teams.SelectMany(t => t.Players).FirstOrDefault(p => p.Id == room.AdminPlayerId);

                    roomDataService.Room.AdminPlayerId = room.AdminPlayerId;

                    if (oldAdminPlayer is not null) oldAdminPlayer.IsAdmin = false;
                    if (newAdminPlayer is not null) newAdminPlayer.IsAdmin = true;
                }

                roomDataService.Room.JoinCode = room.JoinCode;
                roomDataService.Room.MaxPlayers = room.MaxPlayers;
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> Delete()
    {
        try
        {
            HttpResult result = await roomRestService.DeleteAsync();

            if (result is Success)
            {
                roomDataService.Room = null;
                playerDataService.Player.IsAdmin = false;
                playerDataService.Player.IsOfficer = false;
                playerDataService.Player.TeamId = 0;
                playerDataService.Player.RoomId = 0;
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }
}

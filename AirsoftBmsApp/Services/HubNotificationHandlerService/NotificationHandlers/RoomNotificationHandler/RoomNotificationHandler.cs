using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.RoomNotificationHandler;

public class RoomNotificationHandler : IRoomNotificationHandler
{
    public void OnRoomDeleted(IRoomDataService roomDataService)
    {
        roomDataService.Room = null;
    }

    public void OnRoomJoined(PlayerDto playerDto, ObservableRoom contextRoom)
    {
        bool isPlayerAlreadyInRoom = contextRoom.Teams.SelectMany(t => t.Players).Any(p => p.Id == playerDto.PlayerId);

        if (isPlayerAlreadyInRoom) return;

        contextRoom.Teams.FirstOrDefault()?.Players.Add(new ObservablePlayer(playerDto));
    }

    public void OnRoomUpdated(RoomDto roomDto, ObservableRoom contextRoom)
    {
        contextRoom.AdminPlayerId = roomDto.AdminPlayerId;
        contextRoom.JoinCode = roomDto.JoinCode;
        contextRoom.MaxPlayers = roomDto.MaxPlayers;
    }
}

using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.RoomNotificationHandler;
using Shouldly;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.RoomNotificationHandlerTests;

public class RoomNotificationHandler_OnRoomUpdated_Tests
{
    private readonly RoomNotificationHandler _roomNotificationHandler = new RoomNotificationHandler();

    [Fact]
    public void OnRoomUpdated_RoomIsNotNull_ShouldUpdateRoom()
    {
        // Arrange
        ObservableRoom previousRoom = new()
        {
            Id = 1,
            JoinCode = "000000",
            MaxPlayers = 10,
            AdminPlayerId = null,
        };
        RoomDto roomDto = new()
        {
            RoomId = 1,
            JoinCode = "111111",
            MaxPlayers = 44,
            AdminPlayerId = 3,
        };

        // Act
        _roomNotificationHandler.OnRoomUpdated(roomDto, previousRoom);

        // Assert
        previousRoom.ShouldNotBeNull();
        previousRoom.Id.ShouldBe(1);
        previousRoom.JoinCode.ShouldBe(roomDto.JoinCode);
        previousRoom.MaxPlayers.ShouldBe(roomDto.MaxPlayers);
        previousRoom.AdminPlayerId.ShouldBe(roomDto.AdminPlayerId);
    }
}

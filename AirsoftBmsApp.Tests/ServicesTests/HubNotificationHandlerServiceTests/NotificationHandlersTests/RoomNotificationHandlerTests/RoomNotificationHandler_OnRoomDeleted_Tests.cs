using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.RoomNotificationHandler;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Implementations;
using Shouldly;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.RoomNotificationHandlerTests;

public class RoomNotificationHandler_OnRoomDeleted_Tests
{
    private readonly RoomNotificationHandler _roomNotificationHandler = new RoomNotificationHandler();

    [Fact]
    public void OnRoomDeleted_RoomIsNotNull_ShouldSetRoomToNull()
    {
        // Arrange
        RoomDataService roomDataService = new()
        {
            Room = new()
            {
                Id = 1,
            }
        };

        // Act
        _roomNotificationHandler.OnRoomDeleted(roomDataService);

        // Assert
        roomDataService.Room.ShouldBeNull();
    }
}

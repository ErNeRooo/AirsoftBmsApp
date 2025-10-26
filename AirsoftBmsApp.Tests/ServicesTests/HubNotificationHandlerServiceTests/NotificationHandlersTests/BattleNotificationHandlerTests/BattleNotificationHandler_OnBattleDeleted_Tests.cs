using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.BattleNotificationHandler;
using Shouldly;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.BattleNotificationHandlerTests;

public class BattleNotificationHandler_OnBattleDeleted_Tests
{
    private readonly BattleNotificationHandler _battleNotificationHandler = new BattleNotificationHandler();

    [Fact]
    public void OnBattleDeleted_BattleIsNotNull_ShouldSetBattleToNull()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
            Battle = new ObservableBattle()
            {
                BattleId = 10,
                Name = "Test Battle",
                IsActive = false,
                RoomId = 1,
            }
        };

        // Act
        _battleNotificationHandler.OnBattleDeleted(10, room);

        // Assert
        room.Battle.ShouldBeNull();
    }
}

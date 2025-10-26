using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.BattleNotificationHandler;
using Shouldly;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.BattleNotificationHandlerTests;

public class BattleNotificationHandler_OnBattleCreated_Tests
{
    private readonly BattleNotificationHandler _battleNotificationHandler = new BattleNotificationHandler();

    [Fact]
    public void OnBattleCreated_NoBattle_ShouldSetBattle()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
        };
        BattleDto battleDto = new()
        {
            Name = "Test Battle",
            BattleId = 10,
            IsActive = false,
            RoomId = 1,
        };

        // Act
        _battleNotificationHandler.OnBattleCreated(battleDto, room);

        // Assert
        room.Battle.ShouldNotBeNull();
        room.Battle.Name.ShouldBe(battleDto.Name);
        room.Battle.RoomId.ShouldBe(battleDto.RoomId);
        room.Battle.BattleId.ShouldBe(battleDto.BattleId);
        room.Battle.IsActive.ShouldBe(false);
    }
}

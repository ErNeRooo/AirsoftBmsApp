using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.GeolocationService;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.BattleNotificationHandler;
using Shouldly;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.BattleNotificationHandlerTests;

public class BattleNotificationHandler_OnBattleUpdated_Tests
{
    private readonly BattleNotificationHandler _battleNotificationHandler = new BattleNotificationHandler();

    [Fact]
    public void OnBattleUpdated_BattleIsNotNull_ShouldUpdateBattle()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
            Battle = new()
            {
                BattleId = 5,
                Name = "Old Battle",
                IsActive = true,
                RoomId = 1,
            }
        };
        BattleDto battleDto = new()
        {
            Name = "Test Battle",
            BattleId = 10,
            IsActive = false,
            RoomId = 1,
        };

        IGeolocationService geolocationService = new MockGeolocationService();

        // Act
        _battleNotificationHandler.OnBattleUpdated(battleDto, room, geolocationService);

        // Assert
        room.Battle.ShouldNotBeNull();
        room.Battle.Name.ShouldBe(battleDto.Name);
        room.Battle.RoomId.ShouldBe(battleDto.RoomId);
        room.Battle.BattleId.ShouldBe(battleDto.BattleId);
        room.Battle.IsActive.ShouldBe(false);
    }

    private class MockGeolocationService : IGeolocationService
    {
        public Task Start()
        {
            return Task.CompletedTask;
        }

        public void Stop()
        {
            return;
        }
    }
}

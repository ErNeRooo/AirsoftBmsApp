using AirsoftBmsApp.Model.Dto.Order;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.OrderNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.OrderNotificationHandlerTests;

public class OrderNotificationHandler_OnOrderDeleted_Tests
{
    private readonly OrderNotificationHandler _orderNotificationHandler = new OrderNotificationHandler();

    [Theory]
    [InlineData(1, 1, 0)]
    public void OnOrderDeleted_WhenTheOrderExists_ShouldRemoveOrder(int targetOrderId, int expectedTeamId, int expectedOrderCount)
    {
        // Arrange
        ObservableRoom room = new()
        {
            Teams = new()
            {
                new ObservableTeam { Id = 0 },
                new ObservableTeam
                {
                    Id = 1,
                    Players = new()
                    {
                        new ObservablePlayer
                        {
                            Id = 5,
                            TeamId = 1
                        }
                    },
                    Orders = new()
                    {
                        new()
                        {
                            OrderId = 1,
                            PlayerId = 5,
                            BattleId = 1,
                            Longitude = 50.0,
                            Latitude = 8.0,
                            Accuracy = 2,
                            Bearing = 90,
                            Time = DateTimeOffset.UtcNow
                        }
                    }
                }
            }
        };

        // Act
        _orderNotificationHandler.OnOrderDeleted(targetOrderId, room, () => { });

        // Assert
        ObservableTeam? team = room.Teams
            .FirstOrDefault(t => t.Id == expectedTeamId);

        team.ShouldNotBeNull();
        team.Orders.Count.ShouldBe(expectedOrderCount);
    }
}

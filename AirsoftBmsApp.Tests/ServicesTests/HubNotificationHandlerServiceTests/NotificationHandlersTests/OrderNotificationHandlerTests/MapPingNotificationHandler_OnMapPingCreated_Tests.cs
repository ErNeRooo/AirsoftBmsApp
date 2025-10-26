using AirsoftBmsApp.Model.Dto.Order;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.OrderNotificationHandler;
using Shouldly;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.OrderNotificationHandlerTests;

public class OrderNotificationHandler_OnOrderCreated_Tests
{
    private readonly OrderNotificationHandler _orderNotificationHandler = new OrderNotificationHandler();

    [Fact]
    public void OnOrderCreated_WhenTheOrderDoesNotExist_ShouldAddOrderToTeamOrders()
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
                            Id = 1,
                            TeamId = 1

                        },
                        new ObservablePlayer
                        {
                            Id = 5,
                            TeamId = 1
                        }
                    },
                    Orders = new()
                }
            }
        };
        OrderDto orderDto = new()
        {
            OrderId = 1,
            PlayerId = 5,
            BattleId = 1,
            Longitude = 50.0,
            Latitude = 8.0,
            Accuracy = 2,
            Bearing = 90,
            Time = DateTimeOffset.UtcNow
        };

        // Act
        _orderNotificationHandler.OnOrderCreated(orderDto, room);

        // Assert
        int? playersTeamId = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == orderDto.PlayerId)?.TeamId;

        ObservableTeam? team = room.Teams
            .FirstOrDefault(t => t.Id == playersTeamId);

        team.ShouldNotBeNull();
        team.Orders.Count.ShouldBe(1);
    }

    [Fact]
    public void OnOrderCreated_WhenTheOrderExists_ShouldNotMakeAnyChanges()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Teams = new()
            {
                new ObservableTeam
                {
                    Id = 1,
                    Players = new()
                    {
                        new ObservablePlayer
                        {
                            Id = 1,
                            TeamId = 1

                        },
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
        OrderDto orderDto = new()
        {
            OrderId = 1,
            PlayerId = 5,
            BattleId = 1,
            Longitude = 50.0,
            Latitude = 8.0,
            Accuracy = 2,
            Bearing = 90,
            Time = DateTimeOffset.UtcNow
        };

        // Act
        _orderNotificationHandler.OnOrderCreated(orderDto, room);

        // Assert
        int? playersTeamId = room.Teams
            .SelectMany(t => t.Players)
            .FirstOrDefault(p => p.Id == orderDto.PlayerId)?.TeamId;

        ObservableTeam? team = room.Teams
            .FirstOrDefault(t => t.Id == playersTeamId);

        team.ShouldNotBeNull();
        team.Orders.Count.ShouldBe(1);
    }
}

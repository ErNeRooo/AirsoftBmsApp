using AirsoftBmsApp.Model.Dto.Order;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.OrderNotificationHandler;

public class OrderNotificationHandler : IOrderNotificationHandler
{
    public void OnOrderCreated(OrderDto orderDto, ObservableRoom contextRoom, Action refreshMap)
    {
        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == orderDto.PlayerId);
        ObservableTeam? team = contextRoom.Teams.FirstOrDefault(t => t.Id == player?.TeamId);

        if (team is null || team.Orders.Any(mp => mp.OrderId == orderDto.OrderId)) return;

        team.Orders.Add(new ObservableOrder(orderDto));

        refreshMap();
    }

    public void OnOrderDeleted(int orderId, ObservableRoom contextRoom, Action refreshMap)
    {
        List<ObservableOrder> orders = contextRoom.Teams.SelectMany(t => t.Orders).ToList();
        ObservableOrder? order = orders.FirstOrDefault(mp => mp.OrderId == orderId);

        if (order is null) return;

        List<ObservablePlayer> players = contextRoom.Teams.SelectMany(t => t.Players).ToList();
        ObservablePlayer? player = players.FirstOrDefault(p => p.Id == order.PlayerId);
        ObservableTeam? team = contextRoom.Teams.FirstOrDefault(t => t.Id == player?.TeamId);

        team?.Orders.Remove(order);

        refreshMap();
    }
}

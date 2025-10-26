using AirsoftBmsApp.Model.Dto.Order;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.OrderNotificationHandler;

public interface IOrderNotificationHandler
{
    void OnOrderCreated(OrderDto orderDto, ObservableRoom contextRoom);
    void OnOrderDeleted(int orderId, ObservableRoom contextRoom);
}

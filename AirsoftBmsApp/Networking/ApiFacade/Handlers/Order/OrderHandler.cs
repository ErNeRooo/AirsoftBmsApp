using AirsoftBmsApp.Model.Dto.Order;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.OrderRestService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Order;

public class OrderHandler(IOrderRestService OrderRestService, IRoomDataService roomDataService) : IOrderHandler
{
    public async Task<HttpResult> Create(PostOrderDto postOrderDto)
    {
        try
        {
            (HttpResult result, OrderDto? Order) = await OrderRestService.PostAsync(postOrderDto);

            if (result is Success)
            {
                var players = roomDataService.Room.Teams.SelectMany(t => t.Players);
                    
                foreach (var player in players)
                {
                    if (player.Id == Order?.PlayerId)
                    {
                        player.Orders.Add(new ObservableOrder(Order));
                        break;
                    }
                }
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> Delete(int id)
    {
        try
        {
            HttpResult result = await OrderRestService.DeleteAsync(id);

            if (result is Success)
            {
                roomDataService.Room.Teams.SelectMany(t => t.Players).ToList().ForEach(p =>
                {
                    var orderToRemove = p.Orders.FirstOrDefault(o => o.OrderId == id);
                    if (orderToRemove != null)
                    {
                        p.Orders.Remove(orderToRemove);
                    }
                });
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }
}

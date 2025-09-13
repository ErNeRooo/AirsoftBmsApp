using AirsoftBmsApp.Model.Dto.Order;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.OrderRestService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Order;

public class OrderHandler(IOrderRestService OrderRestService, IRoomDataService roomDataService, IPlayerDataService playerDataService) : IOrderHandler
{
    public async Task<HttpResult> Create(PostOrderDto postOrderDto)
    {
        try
        {
            (HttpResult result, OrderDto? Order) = await OrderRestService.PostAsync(postOrderDto);

            if (result is Success)
            {
                ObservableTeam? team = roomDataService.Room.Teams.FirstOrDefault(t => t.Id == playerDataService.Player.TeamId);

                if (team != null)
                {
                    team.Orders.Add(new ObservableOrder(Order));
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
                var team = roomDataService.Room.Teams
                    .FirstOrDefault(t => t.Id == playerDataService.Player.TeamId);

                var orderToRemove = team?.Orders.FirstOrDefault(o => o.OrderId == id);

                if(orderToRemove is not null && team is not null) team.Orders.Remove(orderToRemove);
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

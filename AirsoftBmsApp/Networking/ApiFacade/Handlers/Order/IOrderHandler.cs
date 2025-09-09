using AirsoftBmsApp.Model.Dto.Order;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Order;

public interface IOrderHandler
{
    public Task<HttpResult> Create(PostOrderDto postOrderDto);
    public Task<HttpResult> Delete(int id);
}

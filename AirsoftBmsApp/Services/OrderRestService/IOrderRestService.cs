using AirsoftBmsApp.Model.Dto.Order;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.OrderRestService;

public interface IOrderRestService
{
    public Task<(HttpResult result, OrderDto Order)> PostAsync(PostOrderDto postOrderDto);
    public Task<HttpResult> DeleteAsync(int id);
}

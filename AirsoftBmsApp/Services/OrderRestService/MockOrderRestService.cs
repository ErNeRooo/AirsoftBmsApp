using AirsoftBmsApp.Model.Dto.Order;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.OrderRestService;

public class MockOrderRestService(IPlayerDataService playerDataService, IRoomDataService roomDataService) : IOrderRestService
{
    public async Task<(HttpResult result, OrderDto Order)> PostAsync(PostOrderDto postOrderDto)
    {
        return (new Success(), new OrderDto()
        {
            PlayerId = postOrderDto.PlayerId,
            BattleId = roomDataService.Room.Battle.BattleId,
            Longitude = postOrderDto.Longitude,
            Latitude = postOrderDto.Latitude,
            Accuracy = postOrderDto.Accuracy,
            Bearing = postOrderDto.Bearing,
            Time = postOrderDto.Time,
            Type = postOrderDto.Type
        });
    }
}

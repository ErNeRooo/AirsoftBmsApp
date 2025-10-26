using AirsoftBmsApp.Model.Dto.Vertex;
using AirsoftBmsApp.Model.Dto.Zone;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.ZoneRestService;

public class MockZoneRestService(IPlayerDataService playerDataService, IRoomDataService roomDataService) : IZoneRestService
{
    private int _nextId = 0;

    public async Task<HttpResult> DeleteAsync(int ZoneId)
    {
        return new Success();
    }

    public async Task<(HttpResult result, ZoneDto Zone)> PostAsync(PostZoneDto postZoneDto)
    {
        _nextId++;

        List<VertexDto> mappedVertices = postZoneDto.Vertices.Select(v => new VertexDto()
        {
            Longitude = v.Longitude,
            Latitude = v.Latitude
        }).ToList();

        return (new Success(), new ZoneDto()
        {
            ZoneId = _nextId,
            BattleId = postZoneDto.BattleId,
            Type = postZoneDto.Type,
            Name = postZoneDto.Name,
            Vertices = mappedVertices
        });
    }

    public async Task<(HttpResult result, ZoneDto Zone)> PutAsync(PutZoneDto putZoneDto, int zoneId)
    {
        List<VertexDto> mappedVertices = putZoneDto.Vertices.Select(v => new VertexDto()
        {
            Longitude = v.Longitude,
            Latitude = v.Latitude
        }).ToList();

        return (new Success(), new ZoneDto()
        {
            ZoneId = zoneId,
            BattleId = putZoneDto.BattleId,
            Type = putZoneDto.Type,
            Name = putZoneDto.Name,
            Vertices = mappedVertices
        });
    }
}

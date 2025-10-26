using AirsoftBmsApp.Model.Dto.Vertex;

namespace AirsoftBmsApp.Model.Dto.Zone;

public class ZoneDto
{
    public int ZoneId { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public int BattleId { get; set; }
    public List<VertexDto> Vertices { get; set; }
}


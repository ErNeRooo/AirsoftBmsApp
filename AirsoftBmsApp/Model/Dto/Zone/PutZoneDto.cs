using AirsoftBmsApp.Model.Dto.Vertex;

namespace AirsoftBmsApp.Model.Dto.Zone;

public class PutZoneDto
{
    public string Type { get; set; }
    public string Name { get; set; }
    public int BattleId { get; set; }
    public List<PostVertexDto> Vertices { get; set; }
}

using AirsoftBmsApp.Model.Dto.Location;

namespace AirsoftBmsApp.Model.Dto.Team;

public class TeamDto
{
    public int TeamId { get; set; }
    public string Name { get; set; }
    public int RoomId { get; set; }
    public int? OfficerPlayerId { get; set; }
}

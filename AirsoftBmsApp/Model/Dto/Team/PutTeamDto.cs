using AirsoftBmsApp.Model.Dto.Location;

namespace AirsoftBmsApp.Model.Dto.Team;

public class PutTeamDto
{
    public string Name { get; set; }
    public int? OfficerPlayerId { get; set; }
}

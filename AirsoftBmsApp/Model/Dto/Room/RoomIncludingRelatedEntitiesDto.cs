using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Team;

namespace AirsoftBmsApp.Model.Dto.Room;

public class RoomIncludingRelatedEntitiesDto
{
    public int RoomId { get; set; }
    public int MaxPlayers { get; set; }
    public string JoinCode { get; set; }
    public PlayerDto? AdminPlayer { get; set; }
    public BattleDto Battle { get; set; }
    public List<PlayerDto> Players { get; set; }
    public List<TeamDto> Teams { get; set; }
    public List<DeathDto> Deaths { get; set; }
    public List<KillDto> Kills { get; set; }
}

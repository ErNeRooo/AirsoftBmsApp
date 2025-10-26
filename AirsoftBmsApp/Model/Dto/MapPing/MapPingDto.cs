namespace AirsoftBmsApp.Model.Dto.MapPing;

public class MapPingDto
{
    public int MapPingId { get; set; }
    public int PlayerId { get; set; }
    public int BattleId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public double Accuracy { get; set; }
    public double Bearing { get; set; }
    public DateTimeOffset Time { get; set; }
}


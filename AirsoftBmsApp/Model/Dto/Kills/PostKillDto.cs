namespace AirsoftBmsApp.Model.Dto.Kills;

public class PostKillDto
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public short Accuracy { get; set; }
    public short Bearing { get; set; }
    public DateTimeOffset Time { get; set; }
}

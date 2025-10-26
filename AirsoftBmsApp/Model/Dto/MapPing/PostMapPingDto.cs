namespace AirsoftBmsApp.Model.Dto.MapPing;

public class PostMapPingDto
{
    public int PlayerId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public double Accuracy { get; set; }
    public double Bearing { get; set; }
    public DateTimeOffset Time { get; set; }
}

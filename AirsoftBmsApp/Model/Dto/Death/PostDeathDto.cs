namespace AirsoftBmsApp.Model.Dto.Death;

public class PostDeathDto
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public short Accuracy { get; set; }
    public short Bearing { get; set; }
    public DateTimeOffset Time { get; set; }
}

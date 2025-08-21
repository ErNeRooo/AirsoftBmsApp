namespace AirsoftBmsApp.Model.Dto.Death;

public class PostDeathDto
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public double Accuracy { get; set; }
    public double Bearing { get; set; }
    public DateTimeOffset Time { get; set; }
}

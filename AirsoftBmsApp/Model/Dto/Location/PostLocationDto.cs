namespace AirsoftBmsApp.Model.Dto.Location;

public class PostLocationDto
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public double Accuracy { get; set; }
    public double Bearing { get; set; }
    public DateTimeOffset Time { get; set; }
    public string Type { get; set; }
}

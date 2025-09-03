namespace AirsoftBmsApp.Model.Dto.Order;

public class PostOrderDto
{
    public int PlayerId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public double Accuracy { get; set; }
    public double Bearing { get; set; }
    public DateTimeOffset Time { get; set; }
    public string Type { get; set; }
}

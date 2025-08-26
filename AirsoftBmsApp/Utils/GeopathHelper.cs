using Microsoft.Maui.Maps;

namespace AirsoftBmsApp.Utils;

public static class GeopathHelper
{
    public static bool IsPointInPolygon(this IGeoPathMapElement polygon, Location point)
    {
        int count = polygon.Count;
        
        bool inside = false;
        
        for (int i = 0, j = count - 1; i < count; j = i++)
        {
            var firstPolygonVertex = polygon[i];
            var secondPolygonVertex = polygon[j];

            if (
                (point.Longitude < firstPolygonVertex.Longitude) != (point.Longitude < secondPolygonVertex.Longitude) &&
                (point.Latitude < (secondPolygonVertex.Latitude - firstPolygonVertex.Latitude) * 
                    (point.Longitude - firstPolygonVertex.Longitude) / 
                    (secondPolygonVertex.Longitude - firstPolygonVertex.Longitude) + 
                    firstPolygonVertex.Latitude)
                )
            {
                inside = !inside;
            }
        }
        
        return inside;
    }
}

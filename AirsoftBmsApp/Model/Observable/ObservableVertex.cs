using AirsoftBmsApp.Model.Dto.Vertex;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Model.Observable;
public partial class ObservableVertex : ObservableObject
{
    [ObservableProperty]
    double longitude;

    [ObservableProperty]
    double latitude;

    public ObservableVertex()
    {
        
    }

    public ObservableVertex(VertexDto vertexDto)
    {
        Longitude = vertexDto.Longitude;
        Latitude = vertexDto.Latitude;
    }
}

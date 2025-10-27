using AirsoftBmsApp.Model.Dto.Zone;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Model.Observable;

public static class ZoneTypes
{
    public const string SPAWN = "SPAWN";
}
public partial class ObservableZone : ObservableObject
{
    [ObservableProperty]
    int zoneId;

    [ObservableProperty]
    string type;

    [ObservableProperty]
    string name;

    [ObservableProperty]
    int battleId;

    public ObservableCollection<ObservableVertex> Vertices = new();

    public ObservableZone()
    {
        
    }

    public ObservableZone(ZoneDto zoneDto)
    {
        ZoneId = zoneDto.ZoneId;
        Type = zoneDto.Type;
        Name = zoneDto.Name;
        BattleId = zoneDto.BattleId;

        foreach (var vertexDto in zoneDto.Vertices)
        {
            Vertices.Add(new ObservableVertex(vertexDto));
        }
    }
}

using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Resources.Styles.TeamTheme;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Maps;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableTeam : ObservableObject, IObservableTeam
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private int roomId;

    [ObservableProperty]
    private int? officerId;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private ObservableCollection<ObservablePlayer> players = new();

    [ObservableProperty]
    private ObservableCollection<ObservableOrder> orders = new();

    [ObservableProperty]
    private ITeamTheme teamTheme = TeamThemes.UnderNoFlag;

    [ObservableProperty]
    private Polygon? spawnZone = null;

    public ObservableTeam()
    {
        
    }

    public ObservableTeam(TeamDto team)
    {
        Id = team.TeamId;
        RoomId = team.RoomId;
        OfficerId = team.OfficerPlayerId ?? 0;
        Name = team.Name;

        if (team.SpawnZoneVertices?.Count() > 0)
        {
            Polygon polygon = new() 
            {
                StrokeColor = TeamTheme.TitleColor,
                FillColor = TeamTheme.TitleColor.WithAlpha(0.4f),
            };

            foreach (var vertex in team.SpawnZoneVertices)
            {
                polygon.Geopath.Add(new Location(vertex.Latitude, vertex.Longitude));
            }

            SpawnZone = polygon;
        }
    }

    public void Attach(ObservablePlayer observer)
    {
        players.Add(observer);
    }

    public void Detach(ObservablePlayer observer)
    {
        players.Remove(observer);
    }

    public void Notify()
    {
        foreach (ObservablePlayer observer in players)
        {
            observer.UpdateIsOfficer(this);
        }
    }

    partial void OnOfficerIdChanged(int? oldValue, int? newValue)
    {
        ObservablePlayer? oldPlayer = Players.FirstOrDefault(p => p.Id == oldValue);
        ObservablePlayer? newPlayer = Players.FirstOrDefault(p => p.Id == newValue);

        if (oldPlayer != null) oldPlayer.UpdateIsOfficer(this);
        if (newPlayer != null) newPlayer.UpdateIsOfficer(this);
    }
}

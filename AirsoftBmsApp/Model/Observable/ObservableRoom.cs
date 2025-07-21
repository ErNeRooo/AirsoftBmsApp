using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Resources.Styles.TeamTheme;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableRoom : ObservableObject, IObservableRoom
{
    [ObservableProperty]
    public int id;

    [ObservableProperty]
    public int maxPlayers;

    [ObservableProperty]
    public string joinCode;
    
    [ObservableProperty]
    private int? adminPlayerId;

    public ObservableCollection<ObservableTeam> Teams { get; set; } = 
        new ObservableCollection<ObservableTeam>()
        {
            new ObservableTeam
            {
                Id = 0,
                Name = "Under No Flag",
                Players = new ObservableCollection<ObservablePlayer>(),
                TeamTheme = TeamThemes.UnderNoFlag,
            },
        };

    public ObservableBattle? Battle { get; set; }

    public ObservableRoom()
    {
        
    }

    public ObservableRoom(RoomDto room)
    {
        Id = room.RoomId;
        MaxPlayers = room.MaxPlayers;
        JoinCode = room.JoinCode;
        AdminPlayerId = room.AdminPlayerId;
    }

    List<IObservablePlayer> observerablePlayers = new();

    public ObservableRoom(RoomIncludingRelatedEntitiesDto room)
    {
        Id = room.RoomId;
        MaxPlayers = room.MaxPlayers;
        JoinCode = room.JoinCode;
        AdminPlayerId = room.AdminPlayer is null ? 0 : room.AdminPlayer.PlayerId;

        Battle = room.Battle is null ? null : new ObservableBattle(room.Battle);

        var fetchedTeams = room.Teams.Select(team => new ObservableTeam(team));
        foreach (var team in fetchedTeams) Teams.Add(team);

        if (room.Players != null)
        {
            foreach (PlayerDto player in room.Players)
            {
                if (player.TeamId is null)
                {
                    ObservablePlayer observablePlayer = new ObservablePlayer(player);

                    if(room.AdminPlayer is not null && player.PlayerId == room.AdminPlayer.PlayerId)
                    {
                        observablePlayer.IsAdmin = true;
                    }

                    observerablePlayers.Add(observablePlayer);
                    Teams[0].Players.Add(observablePlayer);
                    continue;
                }

                var team = Teams.FirstOrDefault(t => t.Id == player.TeamId);
                if (team != null)
                {
                    team.Players.Add(new ObservablePlayer(player));
                }
            }
        }
    }

    public void Attach(IObservablePlayer observer)
    {
        observerablePlayers.Add(observer);
    }

    public void Detach(IObservablePlayer observer)
    {
        observerablePlayers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observerablePlayers)
        {
            observer.Update(this);
        }
    }

    partial void OnAdminPlayerIdChanged(int? oldValue, int? newValue)
    {
        IObservablePlayer? oldPlayer = observerablePlayers.FirstOrDefault(p => p.Id == oldValue);
        IObservablePlayer? newPlayer = observerablePlayers.FirstOrDefault(p => p.Id == newValue);

        if (oldPlayer != null) oldPlayer.Update(this);
        if (newPlayer != null) newPlayer.Update(this);
    }
}

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

public partial class ObservableRoom : ObservableObject
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

    public ObservableRoom(RoomIncludingRelatedEntitiesDto room)
    {
        Id = room.RoomId;
        MaxPlayers = room.MaxPlayers;
        JoinCode = room.JoinCode;
        AdminPlayerId = room.AdminPlayer.PlayerId;

        Battle = room.Battle is null ? null : new ObservableBattle(room.Battle);

        var fetchedTeams = room.Teams.Select(team => new ObservableTeam(team));
        foreach (var team in fetchedTeams) Teams.Add(team);

        if (room.Players != null)
        {
            foreach (PlayerDto player in room.Players)
            {
                if (player.TeamId is null)
                {
                    Teams[0].Players.Add(new ObservablePlayer(player));
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
}

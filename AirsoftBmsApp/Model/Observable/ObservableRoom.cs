using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Resources.Styles.TeamTheme;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

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
                Name = AppResources.UnderNoFlagHeader,
                Players = new ObservableCollection<ObservablePlayer>(),
                TeamTheme = TeamThemes.UnderNoFlag,
            },
        };

    [ObservableProperty]
    public ObservableBattle? battle;

    List<IObservablePlayer> observerablePlayers = new();

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

    public ObservableRoom(RoomIncludingRelatedEntitiesDto room, Action OnBattleActivated, Action OnBattleDeactivated)
    {
        Id = room.RoomId;
        MaxPlayers = room.MaxPlayers;
        JoinCode = room.JoinCode;
        AdminPlayerId = room.AdminPlayer is null ? 0 : room.AdminPlayer.PlayerId;

        Battle = room.Battle is null ? null : new ObservableBattle(room.Battle, OnBattleActivated, OnBattleDeactivated);

        var fetchedTeams = room.Teams.Select(team => new ObservableTeam(team));
        foreach (var team in fetchedTeams) Teams.Add(team);

        if (room.Players == null) return;

        foreach (PlayerDto player in room.Players)
        {
            ObservablePlayer observablePlayer = new(player);

            if (room.AdminPlayer is not null && player.PlayerId == room.AdminPlayer.PlayerId)
            {
                observablePlayer.IsAdmin = true;
            }

            observerablePlayers.Add(observablePlayer);

            if (player.TeamId is null)
            {
                Teams[0].Players.Add(observablePlayer);
                continue;
            }

            if (room.Deaths is not null)
            {
                observablePlayer.Deaths = new ObservableCollection<ObservableDeath>(
                    room.Deaths.Where(death => death.PlayerId == observablePlayer.Id).Select(death => new ObservableDeath(death)));
            }
            if (room.Kills is not null)
            {
                observablePlayer.Kills = new ObservableCollection<ObservableKill>(
                    room.Kills.Where(kill => kill.PlayerId == observablePlayer.Id).Select(kill => new ObservableKill(kill)));
            }
            if (room.Locations is not null)
            {
                observablePlayer.Locations = new ObservableCollection<ObservableLocation>(
                    room.Locations.Where(location => location.PlayerId == observablePlayer.Id).Select(location => new ObservableLocation(location)));
            }

            var team = Teams.FirstOrDefault(t => t.Id == player.TeamId);
            if (team != null)
            {
                if (player.PlayerId == team.OfficerId)
                {
                    observablePlayer.IsOfficer = true;
                }

                team.Players.Add(observablePlayer);
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
            observer.UpdateIsAdmin(this);
        }
    }

    partial void OnAdminPlayerIdChanged(int? oldValue, int? newValue)
    {
        IObservablePlayer? oldPlayer = observerablePlayers.FirstOrDefault(p => p.Id == oldValue);
        IObservablePlayer? newPlayer = observerablePlayers.FirstOrDefault(p => p.Id == newValue);

        if (oldPlayer != null) oldPlayer.UpdateIsAdmin(this);
        if (newPlayer != null) newPlayer.UpdateIsAdmin(this);
    }
}

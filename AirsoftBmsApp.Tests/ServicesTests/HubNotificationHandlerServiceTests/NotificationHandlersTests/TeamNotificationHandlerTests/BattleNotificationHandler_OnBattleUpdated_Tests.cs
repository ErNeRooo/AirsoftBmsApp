using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Dto.Vertex;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.TeamNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.TeamNotificationHandlerTests;

public class TeamNotificationHandler_OnTeamUpdated_Tests
{
    private readonly TeamNotificationHandler _teamNotificationHandler = new TeamNotificationHandler();

    [Fact]
    public void OnTeamUpdated_TheTeamExists_ShouldUpdateTeam()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
            Teams = new ObservableCollection<ObservableTeam>()
            {
                new(){ Id = 0 },
                new()
                {
                    Id = 10,
                    Name = "Old Team",
                    OfficerId = null,
                    RoomId = 1,
                }
            }
        };
        TeamDto teamDto = new()
        {
            TeamId = 10,
            Name = "New Team",
            OfficerPlayerId = 2,
            RoomId = 1,
        };

        // Act
        _teamNotificationHandler.OnTeamUpdated(teamDto, room, () => { });

        // Assert
        ObservableTeam? team = room.Teams.FirstOrDefault(t => t.Id == teamDto.TeamId);

        room.Teams.Count.ShouldBe(2);
        team.ShouldNotBeNull();
        team.Id.ShouldBe(teamDto.TeamId);
        team.Name.ShouldBe(teamDto.Name);
        team.OfficerId.ShouldBe(teamDto.OfficerPlayerId);
        team.RoomId.ShouldBe(teamDto.RoomId);
    }

    [Fact]
    public void OnTeamUpdated_TeamSpawnGiven_ShouldUpdateTeamSpawn()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
            Teams = new ObservableCollection<ObservableTeam>()
            {
                new(){ Id = 0 },
                new()
                {
                    Id = 10,
                    Name = "Old Team",
                    OfficerId = null,
                    RoomId = 1,
                }
            }
        };
        TeamDto teamDto = new()
        {
            TeamId = 10,
            SpawnZone = new()
            {
                ZoneId = 1,
                Name = "Spawn Zone",
                Type = ZoneTypes.SPAWN,
                Vertices = new List<VertexDto>() {
                    new(){ Latitude = 0, Longitude = 0},
                    new(){ Latitude = 1, Longitude = 1},
                    new(){ Latitude = 0, Longitude = 1},
                },
            },
        };

        // Act
        _teamNotificationHandler.OnTeamUpdated(teamDto, room, () => { });

        // Assert
        ObservableTeam? team = room.Teams.FirstOrDefault(t => t.Id == teamDto.TeamId);

        room.Teams.Count.ShouldBe(2);
        team.ShouldNotBeNull();
        team.Id.ShouldBe(teamDto.TeamId);
        team.SpawnZone.ShouldNotBeNull();
        team.SpawnZoneId.ShouldBe(teamDto.SpawnZone.ZoneId);
        team.SpawnZone.Geopath.ShouldBe(new List<Location>()
        {
            new Location(0,0),
            new Location(1,1),
            new Location(0,1),
        });
    }
}

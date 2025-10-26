using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.TeamNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.TeamNotificationHandlerTests;

public class TeamNotificationHandler_OnTeamCreated_Tests
{
    private readonly TeamNotificationHandler _teamNotificationHandler = new TeamNotificationHandler();

    [Fact]
    public void OnTeamCreated_TheTeamDoesNotExist_ShouldSetAddTeam()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
            Teams = new ObservableCollection<ObservableTeam>()
            {
                new(){ Id = 0 }
            }
        };
        TeamDto teamDto = new()
        {
            TeamId = 10,
            Name = "Test Team",
            OfficerPlayerId = null,
            RoomId = 1,
        };

        // Act
        _teamNotificationHandler.OnTeamCreated(teamDto, room);

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
    public void OnTeamCreated_TheTeamExists_ShouldNotMakeAnyChanges()
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
                    Name = "Test Team",
                    OfficerId = null,
                    RoomId = 1,
                }
            }
        };
        TeamDto teamDto = new()
        {
            TeamId = 10,
        };

        // Act
        _teamNotificationHandler.OnTeamCreated(teamDto, room);

        // Assert
        ObservableTeam? team = room.Teams.FirstOrDefault(t => t.Id == teamDto.TeamId);

        room.Teams.Count.ShouldBe(2);
        team.ShouldNotBeNull();
        team.Id.ShouldBe(teamDto.TeamId);
        team.Name.ShouldBe("Test Team");
        team.OfficerId.ShouldBeNull();
        team.RoomId.ShouldBe(1);
    }
}

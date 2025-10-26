using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.TeamNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.TeamNotificationHandlerTests;

public class TeamNotificationHandler_OnTeamDeleted_Tests
{
    private readonly TeamNotificationHandler _teamNotificationHandler = new TeamNotificationHandler();

    [Fact]
    public void OnTeamDeleted_TheTeamExists_ShouldRemoveTeam()
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
        int targetTeamId = 10;

        // Act
        _teamNotificationHandler.OnTeamDeleted(targetTeamId, room);

        // Assert
        ObservableTeam? team = room.Teams.FirstOrDefault(t => t.Id == targetTeamId);

        room.Teams.Count.ShouldBe(1);
        team.ShouldBeNull();
    }
}

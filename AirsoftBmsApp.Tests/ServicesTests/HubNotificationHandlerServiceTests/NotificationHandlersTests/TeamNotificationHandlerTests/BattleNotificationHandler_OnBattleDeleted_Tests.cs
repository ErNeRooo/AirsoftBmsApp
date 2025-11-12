using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.TeamNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.TeamNotificationHandlerTests;

public class TeamNotificationHandler_OnTeamDeleted_Tests
{
    private readonly TeamNotificationHandler _teamNotificationHandler = new TeamNotificationHandler();

    [Fact]
    public void OnTeamDeleted_TheTeamExists_ShouldRemoveTeamAndMovePlayersToUnderNoFlagTeam()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
            Teams = new ObservableCollection<ObservableTeam>()
            {
                new(){ Name = "Under No Flag", Id = 0 },
                new()
                {
                    Id = 2,
                    Name = "Test Team",
                    OfficerId = 1,
                    RoomId = 1,
                    Players = new ObservableCollection<ObservablePlayer>()
                    {
                        new(){ Id = 1, Name = "Player 1", TeamId = 2, RoomId = 1, IsOfficer = true },
                        new(){ Id = 2, Name = "Player 2", TeamId = 2, RoomId = 1 }
                    }
                }
            }
        };
        int targetTeamId = 2;

        // Act
        _teamNotificationHandler.OnTeamDeleted(targetTeamId, room);

        // Assert
        ObservableTeam? team = room.Teams.FirstOrDefault(t => t.Id == targetTeamId);

        room.Teams.Count.ShouldBe(1);
        room.Teams[0].Players.Count.ShouldBe(2); 
        team.ShouldBeNull();
        foreach (ObservablePlayer player in room.Teams[0].Players)
        {
            player.TeamId.ShouldBe(0);
            player.IsOfficer.ShouldBeFalse();
        }
    }
}

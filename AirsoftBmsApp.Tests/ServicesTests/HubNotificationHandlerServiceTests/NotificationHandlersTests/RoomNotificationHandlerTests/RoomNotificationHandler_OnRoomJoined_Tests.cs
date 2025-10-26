using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.RoomNotificationHandler;
using Shouldly;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Tests.ServicesTests.HubNotificationHandlerServiceTests.NotificationHandlersTests.RoomNotificationHandlerTests;

public class RoomNotificationHandler_OnRoomJoined_Tests
{
    private readonly RoomNotificationHandler _roomNotificationHandler = new RoomNotificationHandler();

    [Fact]
    public void OnRoomJoined_PlayerDoesNotExist_ShouldAddPlayerToUnderNoFlagTeam()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
            Teams = new ObservableCollection<ObservableTeam>()
            {
                new() 
                { 
                    Id = 0,
                    Players = new ObservableCollection<ObservablePlayer>()
                    {
                        new() { Id = 2 },
                        new() { Id = 3 },
                    }
                },
                new()
                {
                    Id = 1,
                    Players = new ObservableCollection<ObservablePlayer>()
                    {
                        new() { Id = 1 },
                    }
                }
            }
        };

        PlayerDto joiningPlayer = new()
        {
            PlayerId = 6,
            Name = "test player",
            IsDead = false,
            RoomId = 1,
            TeamId = null,
        };

        // Act
        _roomNotificationHandler.OnRoomJoined(joiningPlayer, room);

        // Assert
        ObservablePlayer? addedPlayer = 
            room.Teams[0].Players.FirstOrDefault(p => p.Id == joiningPlayer.PlayerId);

        addedPlayer.ShouldNotBeNull();
        room.Teams[0].Players.Count.ShouldBe(3);
        room.Teams[1].Players.Count.ShouldBe(1);
        addedPlayer.Name.ShouldBe(joiningPlayer.Name);
        addedPlayer.IsDead.ShouldBe(joiningPlayer.IsDead);
        addedPlayer.TeamId.ShouldBeNull();
        addedPlayer.RoomId.ShouldBe(1);
    }

    [Fact]
    public void OnRoomJoined_ThePlayerAlreadyExists_ShouldNotMakeAnyChanges()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
            Teams = new ObservableCollection<ObservableTeam>()
            {
                new()
                {
                    Id = 0,
                    Players = new ObservableCollection<ObservablePlayer>()
                    {
                        new() { Id = 2 },
                        new() { Id = 3 },
                        new() { 
                            Id = 6 ,
                            Name = "test player",
                            IsDead = false,
                            RoomId = 1,
                            TeamId = null,
                        },
                    }
                },
                new()
                {
                    Id = 1,
                    Players = new ObservableCollection<ObservablePlayer>()
                    {
                        new() { Id = 1 },
                    }
                }
            }
        };

        PlayerDto joiningPlayer = new()
        {
            PlayerId = 6,
        };

        // Act
        _roomNotificationHandler.OnRoomJoined(joiningPlayer, room);

        // Assert
        ObservablePlayer? previousPlayer =
            room.Teams[0].Players.FirstOrDefault(p => p.Id == joiningPlayer.PlayerId);

        previousPlayer.ShouldNotBeNull();
        room.Teams[0].Players.Count.ShouldBe(3);
        room.Teams[1].Players.Count.ShouldBe(1);
        previousPlayer.Name.ShouldBe("test player");
        previousPlayer.IsDead.ShouldBe(false);
        previousPlayer.TeamId.ShouldBeNull();
        previousPlayer.RoomId.ShouldBe(1);
    }

    [Fact]
    public void OnRoomJoined_ThePlayerAlreadyExistsInOtherTeam_ShouldNotMakeAnyChanges()
    {
        // Arrange
        ObservableRoom room = new()
        {
            Id = 1,
            Teams = new ObservableCollection<ObservableTeam>()
            {
                new()
                {
                    Id = 0,
                    Players = new ObservableCollection<ObservablePlayer>()
                    {
                        new() { Id = 2 },
                        new() { Id = 3 },
                    }
                },
                new()
                {
                    Id = 1,
                    Players = new ObservableCollection<ObservablePlayer>()
                    {
                        new() { Id = 1 },
                        new() {
                            Id = 6 ,
                            Name = "test player",
                            IsDead = false,
                            RoomId = 1,
                            TeamId = null,
                        },
                    }
                }
            }
        };

        PlayerDto joiningPlayer = new()
        {
            PlayerId = 6,
        };

        // Act
        _roomNotificationHandler.OnRoomJoined(joiningPlayer, room);

        // Assert
        ObservablePlayer? previousPlayer =
            room.Teams[1].Players.FirstOrDefault(p => p.Id == joiningPlayer.PlayerId);

        previousPlayer.ShouldNotBeNull();
        room.Teams[0].Players.Count.ShouldBe(2);
        room.Teams[1].Players.Count.ShouldBe(2);
        previousPlayer.Name.ShouldBe("test player");
        previousPlayer.IsDead.ShouldBe(false);
        previousPlayer.TeamId.ShouldBeNull();
        previousPlayer.RoomId.ShouldBe(1);
    }
}

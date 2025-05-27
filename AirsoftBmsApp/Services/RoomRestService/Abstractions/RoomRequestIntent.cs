using AirsoftBmsApp.Model.Dto.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.RoomRestService.Abstractions
{
    public abstract record RoomRequestIntent;

    public record GetRoomByIdAsync(int roomId) : RoomRequestIntent;
    public record GetRoomByJoinCodeAsync(string joinCode) : RoomRequestIntent;
    public record PutRoomAsync(PutRoomDto roomDto, int roomId) : RoomRequestIntent;
    public record PostRoomAsync(PostRoomDto roomDto) : RoomRequestIntent;
    public record DeleteRoomAsync(int roomId) : RoomRequestIntent;
    public record JoinRoomAsync(JoinRoomDto roomDto) : RoomRequestIntent;
    public record LeaveRoomAsync() : RoomRequestIntent;
}

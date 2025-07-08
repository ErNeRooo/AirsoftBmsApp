using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;

namespace AirsoftBmsApp.Services.RoomRestService.Implementations
{
    public class MockRoomRestService : IRoomRestService
    {
        public async Task<(HttpResult result, RoomDto? room)> GetByJoinCodeAsync(string joinCode)
        {
            if(joinCode == "400000")
            {
                return (new Failure("400000 - Mocked Bad Request"), null);
            }
            else if (joinCode == "213700")
            {
                throw new Exception("Mocked Exception for Join Code 213700");
            }
            else
            {
                return (new Success(), new RoomDto
                {
                    JoinCode = joinCode,
                    AdminPlayerId = 4,
                    RoomId = 1,
                });
            }
        }

        public async Task<(HttpResult result, RoomDto? room)> GetByIdAsync(int roomId)
        {
            if (roomId == 400)
            {
                return (new Failure("400 - Mocked Bad Request"), null);
            }
            else if (roomId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return (new Success(), new RoomDto
                {
                    JoinCode = "110110",
                    AdminPlayerId = 3,
                    RoomId = 1,
                });
            }
        }

        public async Task<(HttpResult result, RoomDto? room)> PutAsync(PutRoomDto roomDto, int roomId)
        {
            if (roomId == 400)
            {
                return (new Failure("400 - Mocked Bad Request"), null);
            }
            else if (roomId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return (new Success(), new RoomDto
                {
                    JoinCode = roomDto.JoinCode,
                    AdminPlayerId = 3,
                    RoomId = 1,
                });
            }
        }

        public async Task<HttpResult> DeleteAsync(int roomId)
        {
            if(roomId == 400)
            {
                return new Failure("Mocked Bad Request");
            }
            else if (roomId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success();
            }
        }

        public async Task<(HttpResult result, RoomDto? room)> PostAsync(PostRoomDto roomDto)
        {
            if (roomDto.JoinCode == "400000")
            {
                return (new Failure("400000 - Mocked Bad Request"), null);
            }
            else if (roomDto.JoinCode == "213700")
            {
                throw new Exception("Mocked Exception for Join Code 213700");
            }
            else
            {
                return (new Success(), new RoomDto
                {
                    JoinCode = "110110",
                    AdminPlayerId = 3,
                    RoomId = 1,
                });
            }
        }

        public async Task<(HttpResult result, RoomDto? room)> JoinAsync(JoinRoomDto roomDto)
        {
            if (roomDto.JoinCode == "400000")
            {
                return (new Failure("400000 - Mocked Bad Request"), null);
            }
            else if (roomDto.JoinCode == "213700")
            {
                throw new Exception("Mocked Exception for Join Code 213700");
            }
            else
            {
                return (new Success(), new RoomDto
                {
                    JoinCode = "110110",
                    AdminPlayerId = 3,
                    RoomId = 1,
                });
            }
        }

        public async Task<HttpResult> LeaveAsync()
        {
            return new Success();
        }
    }
}

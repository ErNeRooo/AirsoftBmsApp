using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomRestService.Abstractions;

namespace AirsoftBmsApp.Services.RoomRestService.Implementations
{
    public class MockRoomRestService : IRoomRestService
    {
        public async Task<HttpResult> TryRequest(RoomRequestIntent roomRequest)
        {
            try
            {
                await Task.Delay(1000);

                switch (roomRequest)
                {
                    case GetRoomByIdAsync getById:
                        return await GetByIdAsync(getById.roomId);
                    case GetRoomByJoinCodeAsync getByJoinCode:
                        return await GetByJoinCodeAsync(getByJoinCode.joinCode);
                    case PutRoomAsync put:
                        return await PutAsync(put.roomDto, put.roomId);
                    case DeleteRoomAsync delete:
                        return await DeleteAsync(delete.roomId);
                    case PostRoomAsync post:
                        return await PostAsync(post.roomDto);
                    case JoinRoomAsync post:
                        return await JoinAsync(post.roomDto);
                    case LeaveRoomAsync post:
                        return await LeaveAsync();
                    default:
                        return new Failure("Unknown request type");
                }
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
            
        }

        public async Task<HttpResult> GetByJoinCodeAsync(string joinCode)
        {
            if(joinCode == "400000")
            {
                return new Failure("400000 - Mocked Bad Request");
            }
            else if (joinCode == "213700")
            {
                throw new Exception("Mocked Exception for Join Code 213700");
            }
            else
            {
                return new Success<RoomDto>(new RoomDto
                {
                    JoinCode = joinCode,
                    AdminPlayerId = 1,
                    RoomId = 1,
                });
            }
        }

        public async Task<HttpResult> GetByIdAsync(int roomId)
        {
            if (roomId == 400)
            {
                return new Failure("400 - Mocked Bad Request");
            }
            else if (roomId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success<RoomDto>(new RoomDto
                {
                    JoinCode = "110110",
                    AdminPlayerId = 1,
                    RoomId = 1,
                });
            }
        }

        public async Task<HttpResult> PutAsync(PutRoomDto roomDto, int roomId)
        {
            if (roomId == 400)
            {
                return new Failure("400 - Mocked Bad Request");
            }
            else if (roomId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success<object>(null);
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
                return new Success<object>(null);
            }
        }

        private async Task<HttpResult> PostAsync(PostRoomDto roomDto)
        {
            if (roomDto.JoinCode == "400000")
            {
                return new Failure("400000 - Mocked Bad Request");
            }
            else if (roomDto.JoinCode == "213700")
            {
                throw new Exception("Mocked Exception for Join Code 213700");
            }
            else
            {
                return new Success<int>(1);
            }
        }

        private async Task<HttpResult> JoinAsync(JoinRoomDto roomDto)
        {
            if (roomDto.JoinCode == "400000")
            {
                return new Failure("400000 - Mocked Bad Request");
            }
            else if (roomDto.JoinCode == "213700")
            {
                throw new Exception("Mocked Exception for Join Code 213700");
            }
            else
            {
                return new Success<RoomDto>(new RoomDto
                {
                    JoinCode = roomDto.JoinCode,
                    AdminPlayerId = 1,
                    RoomId = 1,
                });
            }
        }

        private async Task<HttpResult> LeaveAsync()
        {
            return new Success<object>(null);
        }
    }
}

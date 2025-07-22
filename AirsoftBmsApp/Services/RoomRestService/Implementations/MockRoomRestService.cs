using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Services.RoomRestService.Implementations
{
    public class MockRoomRestService(
        IRoomDataService roomDataService,
        IPlayerDataService playerDataService
        ) : IRoomRestService
    {
        public async Task<(HttpResult result, RoomIncludingRelatedEntitiesDto? room)> GetByJoinCodeAsync(string joinCode)
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
                return (new Success(), new RoomIncludingRelatedEntitiesDto
                {
                    JoinCode = joinCode,
                    RoomId = 1,
                });
            }
        }

        public async Task<(HttpResult result, RoomIncludingRelatedEntitiesDto? room)> GetByIdAsync(int roomId)
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
                return (new Success(), new RoomIncludingRelatedEntitiesDto
                {
                    JoinCode = "110110",
                    RoomId = 1,
                });
            }
        }

        public async Task<(HttpResult result, RoomDto? room)> PutAsync(PutRoomDto roomDto)
        {
            await Task.Delay(200);

            return (new Success(), new RoomDto
            {
                JoinCode = roomDto.JoinCode,
                AdminPlayerId = roomDto.AdminPlayerId ?? roomDataService.Room.AdminPlayerId,
                RoomId = 1,
            });
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
                    AdminPlayerId = 1,
                    MaxPlayers = 10,
                    RoomId = 1,
                });
            }
        }

        public async Task<(HttpResult result, RoomIncludingRelatedEntitiesDto? room)> JoinAsync(JoinRoomDto roomDto)
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
                return (new Success(), new RoomIncludingRelatedEntitiesDto
                {
                    JoinCode = "000000",
                    RoomId = 1,
                    MaxPlayers = 10,
                    AdminPlayer = null,
                    Battle = new BattleDto
                    {
                        BattleId = 1,
                        IsActive = false,
                        Name = "Mocked Battle",
                    },
                    Players = new List<PlayerDto>
                    {
                        new PlayerDto
                        {
                            PlayerId = 3,
                            Name = "Eris",
                            TeamId = 1,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 2,
                            Name = "Narberal",
                            TeamId = 1,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 4,
                            Name = "Yor",
                            TeamId = 1,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 10,
                            Name = "Alya",
                            TeamId = 1,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 11,
                            Name = "Fern",
                            TeamId = 1,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 12,
                            Name = "Solution",
                            TeamId = 1,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 13,
                            Name = "Mikasa",
                            TeamId = 1,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 19,
                            Name = "Chisato",
                            TeamId = 1,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 21,
                            Name = "Takina",
                            TeamId = 1,
                            RoomId = 1,
                        },

                        new PlayerDto
                        {
                            PlayerId = 5,
                            Name = "Sparkle",
                            TeamId = null,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 1,
                            Name = playerDataService.Player.Name,
                            TeamId = null,
                            RoomId = 1,
                        },

                        new PlayerDto
                        {
                            PlayerId = 6,
                            Name = "Yumemizuki Mizuki",
                            TeamId = 2,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 8,
                            Name = "Yae Miko",
                            TeamId = 2,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 7,
                            Name = "Ei",
                            TeamId = 2,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 9,
                            Name = "Keqing",
                            TeamId = 2,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 14,
                            Name = "Yelan",
                            TeamId = 2,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 15,
                            Name = "Ningguang",
                            TeamId = 2,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 16,
                            Name = "Mona",
                            TeamId = 2,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 17,
                            Name = "Navia",
                            TeamId = 2,
                            RoomId = 1,
                        },
                        new PlayerDto
                        {
                            PlayerId = 18,
                            Name = "Clorinde",
                            TeamId = 2,
                            RoomId = 1,
                        },
                    },
                    Teams = new List<TeamDto>
                    {
                        new TeamDto
                        {
                            TeamId = 1,
                            Name = "Team A",
                            OfficerPlayerId = 4,
                        },
                        new TeamDto
                        {
                            TeamId = 2,
                            Name = "Team B",
                            OfficerPlayerId = null,
                        },
                        new TeamDto
                        {
                            TeamId = 400,
                            Name = "Team Error"
                        }
                    }
                });
            }
        }

        public async Task<HttpResult> LeaveAsync()
        {
            return new Success();
        }
    }
}

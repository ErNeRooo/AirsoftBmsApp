using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Model.Dto.Kills;
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
                JoinCode = string.IsNullOrEmpty(roomDto.JoinCode) ? roomDataService.Room.JoinCode : roomDto.JoinCode,
                AdminPlayerId = roomDto.AdminPlayerId ?? roomDataService.Room.AdminPlayerId,
                MaxPlayers = roomDto.MaxPlayers ?? roomDataService.Room.MaxPlayers,
                RoomId = 1,
            });
        }

        public async Task<HttpResult> DeleteAsync()
        {
            return new Success();
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
                            IsDead = false,
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
                            IsDead = true,
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
                    },
                    Kills = new List<KillDto>
                    {
                        new KillDto
                        {
                            KillId = 1,
                            PlayerId = 13,
                            BattleId = 1,
                            Longitude = 23.4567,
                            Latitude = 45.6789,
                            Accuracy = 1.95f,
                            Bearing = 180,
                            Time = DateTimeOffset.Now.AddMinutes(-10),
                        },
                        new KillDto
                        {
                            KillId = 2,
                            PlayerId = 4,
                            BattleId = 1,
                            Longitude = 24.1234,
                            Latitude = 45.9876,
                            Accuracy = 2.10f,
                            Bearing = 90,
                            Time = DateTimeOffset.Now.AddMinutes(-8),
                        },
                        new KillDto
                        {
                            KillId = 3,
                            PlayerId = 2,
                            BattleId = 1,
                            Longitude = 24.2345,
                            Latitude = 45.8765,
                            Accuracy = 1.75f,
                            Bearing = 135,
                            Time = DateTimeOffset.Now.AddMinutes(-6),
                        },
                        new KillDto
                        {
                            KillId = 4,
                            PlayerId = 10,
                            BattleId = 1,
                            Longitude = 24.3456,
                            Latitude = 45.7654,
                            Accuracy = 1.50f,
                            Bearing = 45,
                            Time = DateTimeOffset.Now.AddMinutes(-4),
                        },
                        new KillDto
                        {
                            KillId = 5,
                            PlayerId = 6,
                            BattleId = 1,
                            Longitude = 24.4567,
                            Latitude = 45.6543,
                            Accuracy = 1.90f,
                            Bearing = 210,
                            Time = DateTimeOffset.Now.AddMinutes(-2),
                        },
                        new KillDto
                        {
                            KillId = 6,
                            PlayerId = 1,
                            BattleId = 1,
                            Longitude = 24.5678,
                            Latitude = 45.5432,
                            Accuracy = 2.25f,
                            Bearing = 300,
                            Time = DateTimeOffset.Now.AddMinutes(-1),
                        },
                        new KillDto
                        {
                            KillId = 7,
                            PlayerId = 11,
                            BattleId = 1,
                            Longitude = 24.6789,
                            Latitude = 45.4321,
                            Accuracy = 1.60f,
                            Bearing = 75,
                            Time = DateTimeOffset.Now.AddMinutes(-9),
                        },
                        new KillDto
                        {
                            KillId = 8,
                            PlayerId = 12,
                            BattleId = 1,
                            Longitude = 24.7890,
                            Latitude = 45.3210,
                            Accuracy = 2.00f,
                            Bearing = 110,
                            Time = DateTimeOffset.Now.AddMinutes(-7),
                        },
                        new KillDto
                        {
                            KillId = 9,
                            PlayerId = 14,
                            BattleId = 1,
                            Longitude = 24.8901,
                            Latitude = 45.2109,
                            Accuracy = 1.45f,
                            Bearing = 160,
                            Time = DateTimeOffset.Now.AddMinutes(-5),
                        },
                        new KillDto
                        {
                            KillId = 10,
                            PlayerId = 15,
                            BattleId = 1,
                            Longitude = 24.9012,
                            Latitude = 45.1098,
                            Accuracy = 1.80f,
                            Bearing = 25,
                            Time = DateTimeOffset.Now.AddMinutes(-4),
                        },
                        new KillDto
                        {
                            KillId = 11,
                            PlayerId = 16,
                            BattleId = 1,
                            Longitude = 25.0123,
                            Latitude = 45.0087,
                            Accuracy = 2.10f,
                            Bearing = 300,
                            Time = DateTimeOffset.Now.AddMinutes(-3),
                        },
                        new KillDto
                        {
                            KillId = 12,
                            PlayerId = 17,
                            BattleId = 1,
                            Longitude = 25.1234,
                            Latitude = 44.9076,
                            Accuracy = 2.50f,
                            Bearing = 50,
                            Time = DateTimeOffset.Now.AddMinutes(-3),
                        },
                        new KillDto
                        {
                            KillId = 13,
                            PlayerId = 18,
                            BattleId = 1,
                            Longitude = 25.2345,
                            Latitude = 44.8065,
                            Accuracy = 1.70f,
                            Bearing = 190,
                            Time = DateTimeOffset.Now.AddMinutes(-2),
                        },
                        new KillDto
                        {
                            KillId = 14,
                            PlayerId = 8,
                            BattleId = 1,
                            Longitude = 25.3456,
                            Latitude = 44.7054,
                            Accuracy = 1.65f,
                            Bearing = 270,
                            Time = DateTimeOffset.Now.AddMinutes(-2),
                        },
                        new KillDto
                        {
                            KillId = 15,
                            PlayerId = 9,
                            BattleId = 1,
                            Longitude = 25.4567,
                            Latitude = 44.6043,
                            Accuracy = 2.20f,
                            Bearing = 330,
                            Time = DateTimeOffset.Now.AddMinutes(-1),
                        },
                        new KillDto
                        {
                            KillId = 16,
                            PlayerId = 19,
                            BattleId = 1,
                            Longitude = 25.3478,
                            Latitude = 44.5032,
                            Accuracy = 1.95f,
                            Bearing = 120,
                            Time = DateTimeOffset.Now.AddSeconds(-1),
                        },
                        new KillDto
                        {
                            KillId = 17,
                            PlayerId = 19,
                            BattleId = 1,
                            Longitude = 25.3479,
                            Latitude = 44.5032,
                            Accuracy = 1.4f,
                            Bearing = 110,
                            Time = DateTimeOffset.Now.AddSeconds(-2),
                        },
                        new KillDto
                        {
                            KillId = 18,
                            PlayerId = 19,
                            BattleId = 1,
                            Longitude = 25.3480,
                            Latitude = 44.5031,
                            Accuracy = 1.35f,
                            Bearing = 100,
                            Time = DateTimeOffset.Now.AddSeconds(-3),
                        },
                        new KillDto
                        {
                            KillId = 19,
                            PlayerId = 19,
                            BattleId = 1,
                            Longitude = 25.3481,
                            Latitude = 44.5030,
                            Accuracy = 1.08f,
                            Bearing = 90,
                            Time = DateTimeOffset.Now.AddSeconds(-4),
                        },
                    },
                    Deaths = new List<DeathDto>
                    {
                        new DeathDto
                        {
                            DeathId = 1,
                            PlayerId = 13,
                            BattleId = 1,
                            Longitude = 23.4567,
                            Latitude = 45.6789,
                            Accuracy = 1.95f,
                            Bearing = 180,
                            Time = DateTimeOffset.Now.AddMinutes(-10),
                        },
                        new DeathDto
                        {
                            DeathId = 2,
                            PlayerId = 4,
                            BattleId = 1,
                            Longitude = 24.4567,
                            Latitude = 42.6789,
                            Accuracy = 3.95f,
                            Bearing = 18,
                            Time = DateTimeOffset.Now.AddMinutes(-5),
                        },
                        new DeathDto
                        {
                            DeathId = 3,
                            PlayerId = 4,
                            BattleId = 1,
                            Longitude = 24.4567,
                            Latitude = 42.6789,
                            Accuracy = 1.95f,
                            Bearing = 100,
                            Time = DateTimeOffset.Now,
                        },
                        new DeathDto
                        {
                            DeathId = 4,
                            PlayerId = 4,
                            BattleId = 1,
                            Longitude = 24.5567,
                            Latitude = 41.8789,
                            Accuracy = 10f,
                            Bearing = 11,
                            Time = DateTimeOffset.Now.AddMinutes(-1),
                        },
                        new DeathDto
                        {
                            DeathId = 5,
                            PlayerId = 2,
                            BattleId = 1,
                            Longitude = 23.4561,
                            Latitude = 45.6712,
                            Accuracy = 2.3f,
                            Bearing = 90,
                            Time = DateTimeOffset.Now.AddMinutes(-15),
                        },
                        new DeathDto
                        {
                            DeathId = 6,
                            PlayerId = 3,
                            BattleId = 1,
                            Longitude = 23.5561,
                            Latitude = 45.7712,
                            Accuracy = 1.8f,
                            Bearing = 270,
                            Time = DateTimeOffset.Now.AddMinutes(-25),
                        },
                        new DeathDto
                        {
                            DeathId = 7,
                            PlayerId = 10,
                            BattleId = 1,
                            Longitude = 23.6561,
                            Latitude = 45.8712,
                            Accuracy = 1.2f,
                            Bearing = 45,
                            Time = DateTimeOffset.Now.AddMinutes(-30),
                        },
                        new DeathDto
                        {
                            DeathId = 8,
                            PlayerId = 11,
                            BattleId = 1,
                            Longitude = 23.7561,
                            Latitude = 45.9712,
                            Accuracy = 3.5f,
                            Bearing = 135,
                            Time = DateTimeOffset.Now.AddMinutes(-35),
                        },
                        new DeathDto
                        {
                            DeathId = 9,
                            PlayerId = 12,
                            BattleId = 1,
                            Longitude = 23.8561,
                            Latitude = 46.0712,
                            Accuracy = 2.7f,
                            Bearing = 60,
                            Time = DateTimeOffset.Now.AddMinutes(-40),
                        },
                        new DeathDto
                        {
                            DeathId = 10,
                            PlayerId = 6,
                            BattleId = 1,
                            Longitude = 24.4561,
                            Latitude = 46.1712,
                            Accuracy = 1.1f,
                            Bearing = 200,
                            Time = DateTimeOffset.Now.AddMinutes(-20),
                        },
                        new DeathDto
                        {
                            DeathId = 11,
                            PlayerId = 8,
                            BattleId = 1,
                            Longitude = 24.5561,
                            Latitude = 46.2712,
                            Accuracy = 1.6f,
                            Bearing = 10,
                            Time = DateTimeOffset.Now.AddMinutes(-50),
                        },
                        new DeathDto
                        {
                            DeathId = 12,
                            PlayerId = 9,
                            BattleId = 1,
                            Longitude = 24.6561,
                            Latitude = 46.3712,
                            Accuracy = 2.0f,
                            Bearing = 305,
                            Time = DateTimeOffset.Now.AddMinutes(-22),
                        },
                        new DeathDto
                        {
                            DeathId = 13,
                            PlayerId = 17,
                            BattleId = 1,
                            Longitude = 24.7561,
                            Latitude = 46.4712,
                            Accuracy = 2.5f,
                            Bearing = 150,
                            Time = DateTimeOffset.Now.AddMinutes(-18),
                        },
                        new DeathDto
                        {
                            DeathId = 14,
                            PlayerId = 18,
                            BattleId = 1,
                            Longitude = 24.8561,
                            Latitude = 46.5712,
                            Accuracy = 1.9f,
                            Bearing = 315,
                            Time = DateTimeOffset.Now.AddMinutes(-12),
                        },
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

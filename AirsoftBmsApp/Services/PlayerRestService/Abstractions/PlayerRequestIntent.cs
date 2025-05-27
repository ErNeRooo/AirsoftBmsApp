using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.PlayerRestService.Abstractions
{
    public abstract record PlayerRequestIntent;

    public record GetPlayerByIdAsync(int playerId) : PlayerRequestIntent;
    public record PutPlayerAsync(PutPlayerDto playerDto, int playerId) : PlayerRequestIntent;
    public record RegisterPlayerAsync(PostPlayerDto playerDto) : PlayerRequestIntent;
    public record DeletePlayerAsync(int playerId) : PlayerRequestIntent;
}

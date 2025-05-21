using AirsoftBmsApp.Model;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.PlayerDataService.Implementations
{
    public class PlayerDataService : IPlayerDataService
    {
        public Player Player { get; set; } = new();
    }
}

using AirsoftBmsApp.Model;
using AirsoftBmsApp.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.Implementations
{
    public class PlayerDataService : IPlayerDataService
    {
        public Player Player { get; set; }
    }
}

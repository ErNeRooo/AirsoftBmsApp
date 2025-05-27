using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model
{
    public class Room
    {
        public int Id { get; set; }
        public int MaxPlayers { get; set; }
        public string JoinCode { get; set; }
        public int AdminPlayerId { get; set; }

    }
}

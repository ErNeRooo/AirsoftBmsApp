using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model
{
    public class Team
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int OfficerId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public TeamTheme TeamTheme { get; set; }
    }
}

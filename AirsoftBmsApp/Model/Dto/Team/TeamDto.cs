using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Dto.Team
{
    public class TeamDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public int RoomId { get; set; }
        public int? OfficerPlayerId { get; set; }
    }
}

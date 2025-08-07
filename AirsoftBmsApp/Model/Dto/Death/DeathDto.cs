using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Dto.Death
{
    public class DeathDto
    {
        public int DeathId { get; set; }
        public int LocationId { get; set; }
        public int PlayerId { get; set; }
        public int BattleId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Accuracy { get; set; }
        public Int16 Bearing { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}

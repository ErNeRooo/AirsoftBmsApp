using AirsoftBmsApp.Model;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.RoomDataService.Implementations
{
    public class RoomDataService : IRoomDataService
    {
        public ObservableRoom Room { get; set; } = new();
    }
}

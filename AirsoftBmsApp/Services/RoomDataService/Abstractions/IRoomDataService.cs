using AirsoftBmsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.RoomDataService.Abstractions
{
    public interface IRoomDataService
    {
        Room Room { get; set; }
    }
}

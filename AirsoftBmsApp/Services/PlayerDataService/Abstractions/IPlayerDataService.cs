using AirsoftBmsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.PlayerDataService.Abstractions
{
    public interface IPlayerDataService
    {
        ObservablePlayer Player { get; set; }
    }
}

using AirsoftBmsApp.Model;
using AirsoftBmsApp.Resources.Styles;
using AirsoftBmsApp.Resources.Styles.TeamTheme;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.RoomDataService.Implementations
{
    public class RoomDataService : IRoomDataService
    {
        public ObservableRoom Room { get; set; } = new ObservableRoom
        {
            Teams = new ObservableCollection<ObservableTeam>()
            {
                new ObservableTeam
                {
                    Id = 0,
                    Name = "Under No Flag",
                    Players = new ObservableCollection<ObservablePlayer>(),
                    TeamTheme = TeamThemes.UnderNoFlag,
                },
            },
        };
    }
}

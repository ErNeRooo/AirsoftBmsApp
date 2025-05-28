using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model
{
    public partial class ObservableRoom : ObservableObject
    {
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public int maxPlayers;

        [ObservableProperty]
        public string joinCode;
        
        [ObservableProperty]
        private int adminPlayerId;

        public ObservableCollection<ObservableTeam> Teams { get; set; } = new ObservableCollection<ObservableTeam>();
    }
}

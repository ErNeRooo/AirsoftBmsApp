using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirsoftBmsApp.Model;

namespace AirsoftBmsApp.ViewModel.RoomViewModel
{
    public interface IRoomViewModel
    {
        public string JoinCode { get; set; }
        public int Id { get; set; }
        public ObservableCollection<Team> Teams { get; set; }
    }
}

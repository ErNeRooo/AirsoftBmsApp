using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Observable
{
    public interface IObservablePlayer
    {
        int Id { get; set; }
        string Name { get; set; }
        bool IsDead { get; set; }
        bool IsAdmin { get; set; }
        ObservableAccount? Account { get; set; }
        int? TeamId { get; set; }
        int? RoomId { get; set; }

        void Update(IObservableRoom subject);
    }
}

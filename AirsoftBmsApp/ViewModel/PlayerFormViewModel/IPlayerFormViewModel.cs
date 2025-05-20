using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirsoftBmsApp.Model;

namespace AirsoftBmsApp.ViewModel.PlayerFormViewModel
{
    public interface IPlayerFormViewModel
    {
        public Task RegisterPlayerAsync();
    }
}

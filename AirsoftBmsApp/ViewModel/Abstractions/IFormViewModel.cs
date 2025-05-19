using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirsoftBmsApp.Model;

namespace AirsoftBmsApp.ViewModel.Abstractions
{
    public interface IFormViewModel
    {
        public Task RegisterPlayerAsync();
    }
}

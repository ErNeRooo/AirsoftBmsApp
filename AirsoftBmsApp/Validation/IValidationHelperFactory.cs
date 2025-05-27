using AirsoftBmsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Validation
{
    public interface IValidationHelperFactory
    {
        void AddValidations(PlayerForm playerForm);
        void AddValidations(RoomForm roomForm);
    }
}

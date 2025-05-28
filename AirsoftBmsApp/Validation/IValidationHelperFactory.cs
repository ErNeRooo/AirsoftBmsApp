using AirsoftBmsApp.Model.Validatable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Validation
{
    public interface IValidationHelperFactory
    {
        void AddValidations(ValidatablePlayerForm playerForm);
        void AddValidations(ValidatableRoomForm roomForm);
    }
}

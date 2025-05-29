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
        void AddValidations(ValidatablePlayerForm form);
        void AddValidations(ValidatableCreateRoomForm form);
        void AddValidations(ValidatableJoinRoomForm form);
        void AddValidations(ValidatableTeamForm form);
    }
}

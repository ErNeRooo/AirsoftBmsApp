using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Validation.ValidationHelpers
{
    public class JoinRoomFormValidationHelper
    {
        public void AddAllValidations(ValidatableJoinRoomForm roomForm)
        {
            AddJoinCodeValidations(roomForm);
        }

        private void AddJoinCodeValidations(ValidatableJoinRoomForm roomForm)
        {
            roomForm.JoinCode.Validations.Add(new HasLengthRule<string>
            {
                ValidationMessage = "Join code must be exactly 6 characters",
                Length = 6
            });
        }
    }
}

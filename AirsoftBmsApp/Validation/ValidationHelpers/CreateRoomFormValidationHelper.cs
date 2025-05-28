using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Validation.ValidationHelpers
{
    public class CreateRoomFormValidationHelper
    {
        public void AddAllValidations(ValidatableRoomForm roomForm)
        {
            AddJoinCodeValidations(roomForm);
        }

        private void AddJoinCodeValidations(ValidatableRoomForm roomForm)
        {
            roomForm.JoinCode.Validations.Add(new OptionalLengthRule<string>
            {
                ValidationMessage = "Password must be exactly 6 characters, if provided.",
                Length = 6
            });
        }
    }
}

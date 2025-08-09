using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Resources.Languages;
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
        public void AddAllValidations(ValidatableCreateRoomForm roomForm)
        {
            AddJoinCodeValidations(roomForm);
        }

        private void AddJoinCodeValidations(ValidatableCreateRoomForm roomForm)
        {
            roomForm.JoinCode.Validations.Add(new OptionalLengthRule<string>
            {
                ValidationMessage = AppResources.JoinCodeHas6CharactersValidationMessage,
                Length = 6
            });
        }
    }
}

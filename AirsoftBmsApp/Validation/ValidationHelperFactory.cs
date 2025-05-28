using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Validation.ValidationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Validation
{
    public class ValidationHelperFactory : IValidationHelperFactory
    {
        public void AddValidations(ValidatablePlayerForm form)
        {
            var validationHelper = new PlayerFormValidationHelper();

            validationHelper.AddAllValidations(form);
        }

        public void AddValidations(ValidatableCreateRoomForm form)
        {
            var validationHelper = new CreateRoomFormValidationHelper();

            validationHelper.AddAllValidations(form);
        }

        public void AddValidations(ValidatableJoinRoomForm form)
        {
            var validationHelper = new JoinRoomFormValidationHelper();

            validationHelper.AddAllValidations(form);
        }
    }
}

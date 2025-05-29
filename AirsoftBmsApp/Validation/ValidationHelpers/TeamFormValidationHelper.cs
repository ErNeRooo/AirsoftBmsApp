using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Validation.ValidationHelpers
{
    public class TeamFormValidationHelper
    {
        public void AddAllValidations(ValidatableTeamForm teamForm)
        {
            AddNameValidations(teamForm);
        }

        public void AddNameValidations(ValidatableTeamForm teamForm)
        {
            teamForm.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Team name is required."
            });

            teamForm.Name.Validations.Add(new HasMaxLengthRule<string>
            {
                ValidationMessage = "Team name must be 20 characters or fewer.",
                MaxLength = 20
            });
        }
    }
}

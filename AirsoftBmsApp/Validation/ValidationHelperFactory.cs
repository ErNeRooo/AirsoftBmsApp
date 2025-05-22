using AirsoftBmsApp.Model;
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
        public void AddValidations(PlayerForm playerForm)
        {
            var validationHelper = new PlayerFormValidationHelper();

            validationHelper.AddAllValidations(playerForm);
        }
    }
}

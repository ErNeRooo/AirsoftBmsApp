using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AirsoftBmsApp.Validation;

namespace AirsoftBmsApp.Model.Validatable
{
    public class ValidatableTeamForm
    {
        public ValidatableObject<string> Name { get; set; } = new();
    }
}

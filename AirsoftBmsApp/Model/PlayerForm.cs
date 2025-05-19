using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AirsoftBmsApp.Validation;

namespace AirsoftBmsApp.Model
{
    public class PlayerForm
    {
        public ValidatableObject<string> Name { get; set; } = new();
        public ValidatableObject<string> Email { get; set; } = new();
        public ValidatableObject<string> Password { get; set; } = new();
        public ValidatableObject<string> ConfirmPassword { get; set; } = new();
    }
}

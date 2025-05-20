using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Validation.Rules
{
    public class HasLengthRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public int Length { get; set; }

        public bool Check(T value) =>
            value is not null && value.ToString().Length == Length;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Validation.Rules
{
    public class OptionalLengthRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public int Length { get; set; }

        public bool Check(T value)
        {
            var str = value as string;
            if (string.IsNullOrEmpty(str))
                return true;

            return str.Length == Length;
        }
    }
}

﻿using AirsoftBmsApp.Validation;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Validatable
{
    public class ValidatableCreateRoomForm
    {
        public ValidatableObject<string> JoinCode { get; set; } = new();
        public ValidatableObject<string> Password { get; set; } = new();
    }
}

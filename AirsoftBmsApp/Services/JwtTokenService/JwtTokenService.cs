﻿using AirsoftBmsApp.Model;
using AirsoftBmsApp.Services.TeamRestService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.JwtTokenService
{
    public class JwtTokenService : IJwtTokenService
    {
        public string? Token { get; set; } 
    }
}

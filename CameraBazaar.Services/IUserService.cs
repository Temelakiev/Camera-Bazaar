﻿using CameraBazaar.Services.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace CameraBazaar.Services
{
    public interface IUserService
    {
        UserDetails Details(string id);
    }
}

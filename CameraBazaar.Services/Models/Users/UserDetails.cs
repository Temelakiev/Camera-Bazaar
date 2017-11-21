using CameraBazaar.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CameraBazaar.Services.Models.Users
{
    public class UserDetails
    {
        public string Id { get; set; }

        public string Usename { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime LastLogin = DateTime.UtcNow;

        public List<Camera> Cameras { get; set; } 
    }
}

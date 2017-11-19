using CameraBazaar.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CameraBazaar.Services.Models.Users
{
    public class Details
    {
        public string Usename { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Display(Name = "Cameras")]
        public string Camera { get; set; }

        public List<Camera> Cameras { get; set; } 
    }
}

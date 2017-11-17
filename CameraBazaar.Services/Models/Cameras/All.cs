using System;
using System.Collections.Generic;
using System.Text;

namespace CameraBazaar.Services.Models.Cameras
{
    public class All
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}

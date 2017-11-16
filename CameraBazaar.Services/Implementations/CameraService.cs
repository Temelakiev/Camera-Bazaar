using System;
using System.Collections.Generic;
using System.Text;
using CameraBazaar.Data.Models;
using CameraBazaar.Data;
using System.Linq;

namespace CameraBazaar.Services.Implementations
{
    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext db;

        public CameraService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public void Create(CameraMakeType make, string model, decimal price, int quantity, int minShutterSpeed, int maxShutterSpeed, MinISOType minISO, int maxISO, bool isFullFrame, string videoResulution,IEnumerable<LightMetering> lightMetering, string description, string imageUrl, string userId)
        {
            var camera = new Camera
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinISO = minISO,
                MaxISO = maxISO,
                IsFullFrame = isFullFrame,
                VideoResulution = videoResulution,
                LightMetering = (LightMetering)lightMetering.Cast<int>().Sum(),
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.db.Add(camera);
            this.db.SaveChanges();
        }
    }
}

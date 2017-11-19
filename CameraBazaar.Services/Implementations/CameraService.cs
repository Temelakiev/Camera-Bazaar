using System;
using System.Collections.Generic;
using System.Text;
using CameraBazaar.Data.Models;
using CameraBazaar.Data;
using System.Linq;
using CameraBazaar.Services.Models.Cameras;

namespace CameraBazaar.Services.Implementations
{
    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext db;

        public CameraService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<All> All()
            => this.db
            .Cameras
            .OrderBy(c => c.Id)
            .Select(p => new All
            {
                Id=p.Id,
                Make = p.Make.ToString(),
                Model = p.Model,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Quantity = p.Quantity
            })
            .ToList();

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

        public Details Details(int id)
            => this.db
            .Cameras
            .Where(c => c.Id == id)
            .Select(c => new Details
            {
                Make = c.Make,
                Model = c.Model,
                Price = c.Price,
                Quantity = c.Quantity,
                MinShutterSpeed = c.MinShutterSpeed,
                MaxShutterSpeed = c.MaxShutterSpeed,
                MinISO = c.MinISO,
                MaxISO = c.MaxISO,
                IsFullFrame = c.IsFullFrame,
                VideoResulution = c.VideoResulution,
                LightMetering = c.LightMetering,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                User=c.User
            })
            .FirstOrDefault();

    }
}

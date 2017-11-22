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

        public IEnumerable<CameraAll> All()
            => this.db
            .Cameras
            .OrderBy(c => c.Id)
            .Select(p => new CameraAll
            {
                Id=p.Id,
                Make = p.Make.ToString(),
                Model = p.Model,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Quantity = p.Quantity
            })
            .ToList();

        public Camera ById(int id)
            => this.db.Cameras.Where(c => c.Id == id).FirstOrDefault();

        public bool CameraExists(int id, string userId)
            => this.db.Cameras.Any(c => c.Id == id && c.UserId == userId);

        public void Create(CameraMakeType make, string model, decimal price, int quantity, int minShutterSpeed, int maxShutterSpeed, MinISOType minISO, int maxISO, bool isFullFrame, string videoResulution,IEnumerable<LightMetering> lightMetering, string description, string imageUrl, string userId)
        {
            if (lightMetering==null)
            {
                lightMetering = new List<LightMetering>();
            }

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

        public void Delete(int id)
        {
            var camera= this.db.Cameras.Find(id);
            if (camera == null)
            {
                return;
            }

            this.db.Cameras.Remove(camera);
            this.db.SaveChanges();
        }

        public CameraDetails Details(int id)
            => this.db
            .Cameras
            .Where(c => c.Id == id)
            .Select(c => new CameraDetails
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

        public bool Edit(int id,CameraMakeType make, string model, decimal price, int quantity, int minShutterSpeed, int maxShutterSpeed, MinISOType minISO, int maxISO, bool isFullFrame, string videoResulution, IEnumerable<LightMetering> lightMetering, string description, string imageUrl, string userId)
        {
            var camera = this.db.Cameras.FirstOrDefault(c=>c.Id==id && c.UserId==userId);

            if (camera==null)
            {
                return false; ;
            }

            camera.Make = make;
            camera.Model = model;
            camera.Price = price;
            camera.Quantity = quantity;
            camera.MinShutterSpeed = minShutterSpeed;
            camera.MaxShutterSpeed = maxShutterSpeed;
            camera.MinISO = minISO;
            camera.MaxISO = maxISO;
            camera.IsFullFrame = isFullFrame;
            camera.VideoResulution = videoResulution;
            camera.LightMetering = (LightMetering)lightMetering.Cast<int>().Sum();
            camera.Description = description;
            camera.ImageUrl = imageUrl;
            camera.UserId = userId;

            this.db.SaveChanges();

            return true;
        }
    }
}

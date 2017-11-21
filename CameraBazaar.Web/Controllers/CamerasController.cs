using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CameraBazaar.Web.Models.CamerasViewModels;
using CameraBazaar.Services;
using Microsoft.AspNetCore.Identity;
using CameraBazaar.Data.Models;
using CameraBazaar.Services.Models.Cameras;
using CameraBazaar.Web.Infrastructure.Filters;
using CameraBazaar.Data;

namespace CameraBazaar.Web.Controllers
{
    public class CamerasController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ICameraService cameras;
        private readonly CameraBazaarDbContext db;

        public CamerasController(UserManager<User> userManager, ICameraService cameras, CameraBazaarDbContext db)
        {
            this.userManager = userManager;
            this.cameras = cameras;
            this.db = db;
        }

        [Authorize]
        [MeasureTime]
        public IActionResult Add()
            => View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCameraViewModel cameraModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cameraModel);
            }

            this.cameras.Create(
                cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.MinISO,
                cameraModel.MaxISO,
                cameraModel.IsFullFrame,
                cameraModel.VideoResulution,
                cameraModel.LightMetering,
                cameraModel.Description,
                cameraModel.ImageUrl,
                this.userManager.GetUserId(User));

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var camera = (Camera)this.cameras.ById(id);

            if (camera==null)
            {
                return NotFound();
            }

            return this.View(new CameraEdit
            {
                Make= camera.Make,
                Model= camera.Model,
                Price= camera.Price,
                Quantity= camera.Quantity,
                MinShutterSpeed= camera.MinShutterSpeed,
                MaxShutterSpeed= camera.MaxShutterSpeed,
                MinISO= camera.MinISO,
                MaxISO= camera.MaxISO,
                IsFullFrame= camera.IsFullFrame,
                VideoResulution= camera.VideoResulution,
                Description= camera.Description,
                ImageUrl= camera.ImageUrl,
            });
        }

        [HttpPost]
        [Authorize]
        IActionResult Edit(int id, CameraEdit modelCamera)
        {
            if (!ModelState.IsValid)
            {
                return View(modelCamera);
            }

            this.cameras.Edit
                (
                    id,
                    modelCamera.Make,
                    modelCamera.Model,
                    modelCamera.Price,
                    modelCamera.Quantity,
                    modelCamera.MinShutterSpeed,
                    modelCamera.MaxShutterSpeed,
                    modelCamera.MinISO,
                    modelCamera.MaxISO,
                    modelCamera.IsFullFrame,
                    modelCamera.VideoResulution,
                    modelCamera.LightMetering,
                    modelCamera.Description,
                    modelCamera.ImageUrl,
                    this.userManager.GetUserId(User)
                );

            return RedirectToAction(nameof(CamerasController.All));
        }

        public IActionResult Delete(int id) => View(id);

        public IActionResult Destroy(int id)
        {
            this.cameras.Delete(id);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All()
            => View(this.cameras.All());

        public IActionResult Details( int id)
        {
            var camera = this.cameras.Details(id);


            return View(new CameraDetails
            {
                Make=camera.Make,
                Model=camera.Model,
                Price=camera.Price,
                Quantity=camera.Quantity,
                MinShutterSpeed=camera.MinShutterSpeed,
                MaxShutterSpeed=camera.MaxShutterSpeed,
                MinISO=camera.MinISO,
                MaxISO=camera.MaxISO,
                IsFullFrame=camera.IsFullFrame,
                VideoResulution=camera.VideoResulution,
                LightMetering=camera.LightMetering,
                Description=camera.Description,
                ImageUrl=camera.ImageUrl,
                SellerName=camera.User.UserName,
                User=camera.User
            });
        }

        
    }
}

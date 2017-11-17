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

namespace CameraBazaar.Web.Controllers
{
    public class CamerasController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ICameraService cameras;

        public CamerasController(UserManager<User> userManager, ICameraService cameras)
        {
            this.userManager = userManager;
            this.cameras = cameras;
        }

        [Authorize]
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

        public IActionResult All()
            => View(this.cameras.All());

        public IActionResult Details( int id)
        {
            var camera = this.cameras.Details(id);

            return View(new Details
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
                UserId=this.userManager.GetUserId(User)
        });
        }
    }
}

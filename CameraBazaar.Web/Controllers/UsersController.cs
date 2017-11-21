using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CameraBazaar.Services;
using CameraBazaar.Data;
using CameraBazaar.Services.Models.Users;
using Microsoft.AspNetCore.Authorization;

namespace CameraBazaar.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService user;
        private readonly CameraBazaarDbContext db;

        public UsersController(IUserService user, CameraBazaarDbContext db)
        {
            this.user = user;
            this.db = db;
        }

        public IActionResult Details(string id)
        {
            var user = this.db.Users.Where(u => u.Id == id).FirstOrDefault();
            var cameras = this.db.Cameras.Where(c => c.UserId == user.Id).ToList();

            return View(new UserDetails
            {
                Id = user.Id,
                Usename = user.UserName,
                LastLogin=user.LastLogin,
                Cameras = cameras,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }


        public IActionResult Edit(string id)
        {
            var user = this.db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }


            return View(new UserEditProfile
            {
                Id = user.Id,
                Email = user.Email,
                Phone = user.PhoneNumber

            });
        }

        [HttpPost]
        public IActionResult Edit(string id, string password)
        {
            var user = this.db.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user.PasswordHash == password.GetHashCode().ToString() || password != null)
            {
                return RedirectToAction(nameof(Editor));
            }

            return View();
        }

        public IActionResult Editor(string id)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CameraBazaar.Services.Models.Users;
using CameraBazaar.Data;
using System.Linq;
using CameraBazaar.Services.Models.Cameras;

namespace CameraBazaar.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly CameraBazaarDbContext db;

        public UserService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public UserDetails Details(string id)
            => this.db
            .Users
            .Where(u => u.Id == id)
            .Select(u => new UserDetails
            {
                Usename = u.UserName,
                Email = u.Email,
                LastLogin=u.LastLogin,
                PhoneNumber = u.PhoneNumber,
                Cameras = u.Cameras
            })
            .FirstOrDefault();

        //
        public void EditProfile(string id, string password)
        {
            var user = this.db.Users.Find(id);



            user.PasswordHash = password.GetHashCode().ToString();

            this.db.SaveChanges();
        }
            
    }
}

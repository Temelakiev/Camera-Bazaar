using System;
using System.Collections.Generic;
using System.Text;
using CameraBazaar.Services.Models.Users;
using CameraBazaar.Data;
using System.Linq;

namespace CameraBazaar.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly CameraBazaarDbContext db;

        public UserService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        //public Details Details(string id)
        //    => this.db
        //    .Users
        //    .Where(u => u.Id == id)
        //    .Select(u => new Details
        //    {
        //        Usename=u.UserName,
        //        Email=u.Email,
        //        PhoneNumber=u.PhoneNumber,
        //        Camera=$@"{this.db.Users.Where(us=>u.Id==id).Select(us=>u.Cameras.Where(c=>c.Quantity>1).Count())} in stock / {this.db.Users.Where(us => u.Id == id).Select(us => u.Cameras.Where(c => c.Quantity <= 0).Count())} out of stock",
        //        Cameras=this.db.Users.Where(use=>use.Id==id).Select(use=>use.Cameras)
        //    })
        //    .FirstOrDefault();
    }
}

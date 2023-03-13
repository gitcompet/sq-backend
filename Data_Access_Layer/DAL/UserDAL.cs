using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DAL
{
    public class UserDAL
    {
        public List<User> GetAllUser()
        {
            var db = new CompetenceDbContext();
            return db.User.ToList();
        }

        public User GetUserById(int id)
        {
            var db = new CompetenceDbContext();
            User d = new User();

            d = db.User.FirstOrDefault(x => x.LoginId == id);

            return d;
        }


        public int postUser(User user)
        {
            var db = new CompetenceDbContext();
            System.Diagnostics.Debug.WriteLine("===================================================");
            System.Diagnostics.Debug.WriteLine(user.DateCreat);
            System.Diagnostics.Debug.WriteLine("===================================================");
            db.Add(user);
            db.SaveChanges();
            return user.LoginId;
        }
    }
}

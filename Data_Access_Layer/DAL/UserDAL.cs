using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.DAL
{
    public class UserDAL
    {
        private CompetenceDbContext _DbContext;
        public UserDAL()
        {
            this._DbContext = new CompetenceDbContext();
        }
        public List<User> GetAllUser()
        {
            return _DbContext.User.ToList();
        }

        public User GetUserById(int id)
        {
            User user = _DbContext.User.FirstOrDefault(x => x.LoginId == id);
            return user;
        }

        public User GetUserByUsername(String username)
        {   //LOOKUP WITH NON PRIMARY KEY COLUMNS
            _DbContext= new CompetenceDbContext();
            User user = _DbContext.User.FirstOrDefault(x => x.Login== username);
            return user;
        }

        public User GetUserByEmail(String email)
        {
            User user = _DbContext.User.FirstOrDefault(x => x.Email==email);
            return user;
        }


        public int postUser(User user)
        {          
            var foundUserByUsername = GetUserByUsername(user.Login);
            var foundUserByEmail  = GetUserByEmail(user.Login);
            if (foundUserByUsername == null && foundUserByEmail == null)
            {
                _DbContext.Add(user);
                _DbContext.SaveChanges();
                return user.LoginId;
            }
            return -1;           
        }
    }
}

using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public User GetUserByUsername(String username)
        {   //LOOKUP WITH NON PRIMARY KEY COLUMNS
            var db = new CompetenceDbContext();
            User user = db.User.FirstOrDefault(x => x.Login == username);
            return user;
        }

        public User PostUser(User user)
        {
            var db = new CompetenceDbContext();
            db.Add(user);
            db.SaveChanges();
            return (user);
        }

        public User PatchUser(int id, JsonPatchDocument<User> userModelJSON)
        {
            var db = new CompetenceDbContext();
            User d = new User();

            d = db.User.FirstOrDefault(x => x.LoginId == id);
            userModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public User PutUser(User user)
        {
            var db = new CompetenceDbContext();
            User d = new User();
            try
            {
                d = db.User.First(x => x.LoginId == user.LoginId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, user.GetType().GetProperty(property.Name).GetValue(user));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(user);
                db.SaveChanges();
                d = user;
            }
            return d;
        }

        public void DeleteUser(int id)
        {
            var db = new CompetenceDbContext();
            User d = new User();
            d = this.GetUserById(id);
            db.User.Remove(d);
            db.SaveChanges();
        }
    }
}


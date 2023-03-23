using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class UserTypeDAL
    {
        public List<UserType> GetAllUserType()
        {
            var db = new CompetenceDbContext();
            return db.UserType.ToList();
        }

        public UserType GetUserTypeById(int id)
        {
            var db = new CompetenceDbContext();
            UserType d = new UserType();

            d = db.UserType.FirstOrDefault(x => x.UserTypeId == id);

            return d;
        }


        public UserType PostUserType(UserType userType)
        {
            var db = new CompetenceDbContext();
            db.Add(userType);
            db.SaveChanges();
            return (userType);
        }

        public UserType PatchUserType(int id, JsonPatchDocument<UserType> userTypeModelJSON)
        {
            var db = new CompetenceDbContext();
            UserType d = new UserType();

            d = db.UserType.FirstOrDefault(x => x.UserTypeId == id);
            userTypeModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public UserType PutUserType(UserType userType)
        {
            var db = new CompetenceDbContext();
            UserType d = new UserType();
            try
            {
                d = db.UserType.First(x => x.UserTypeId == userType.UserTypeId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, userType.GetType().GetProperty(property.Name).GetValue(userType));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(userType);
                db.SaveChanges();
                d = userType;
            }
            return d;
        }

        public void DeleteUserType(int id)
        {
            var db = new CompetenceDbContext();
            UserType d = new UserType();
            d = this.GetUserTypeById(id);
            db.UserType.Remove(d);
            db.SaveChanges();
        }
    }
}


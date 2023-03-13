using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data_Access_Layer
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


        public void postUserType(UserType usertype)
        {
            var db = new CompetenceDbContext();
            db.Add(usertype);
            db.SaveChanges();
        }

    }
}


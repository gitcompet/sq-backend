using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class TestUserDAL
    {
        public List<TestUser> GetAllTestUser()
        {
            var db = new CompetenceDbContext();
            return db.TestUser.ToList();
        }

        public TestUser GetTestUserById(int id)
        {
            var db = new CompetenceDbContext();
            TestUser d = new TestUser();

            d = db.TestUser.FirstOrDefault(x => x.TestUserId == id);

            return d;
        }

        public IEnumerable<TestUser> GetTestUserByUserId(int id)
        {
            var db = new CompetenceDbContext();
            System.Diagnostics.Debug.WriteLine(id);
            var d = db.TestUser.Where(x => x.LoginId == id);

            return d;
        }


        public TestUser PostTestUser(TestUser testUser)
        {
            var db = new CompetenceDbContext();
            db.Add(testUser);
            db.SaveChanges();
            return (testUser);
        }

        public TestUser PatchTestUser(int id, JsonPatchDocument<TestUser> testUserModelJSON)
        {
            var db = new CompetenceDbContext();
            TestUser d = new TestUser();

            d = db.TestUser.FirstOrDefault(x => x.TestUserId == id);
            testUserModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public TestUser PutTestUser(TestUser testUser)
        {
            var db = new CompetenceDbContext();
            TestUser d = new TestUser();
            try
            {
                d = db.TestUser.First(x => x.TestUserId == testUser.TestUserId);
                foreach(PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, testUser.GetType().GetProperty(property.Name).GetValue(testUser));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(testUser);
                db.SaveChanges();
                d = testUser;
            }
            return d;
        }

        public void DeleteTestUser(int id)
        { 
            var db = new CompetenceDbContext();
            TestUser d = new TestUser();
            d = this.GetTestUserById(id);
            db.TestUser.Remove(d);
            db.SaveChanges();
        }
    }
}


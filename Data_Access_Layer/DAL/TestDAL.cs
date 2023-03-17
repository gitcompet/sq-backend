using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class TestDAL
    {
        public List<Test> GetAllTest()
        {
            var db = new CompetenceDbContext();
            return db.Test.ToList();
        }

        public Test GetTestById(int id)
        {
            var db = new CompetenceDbContext();
            Test d = new Test();

            d = db.Test.FirstOrDefault(x => x.TestId == id);

            return d;
        }


        public Test PostTest(Test test)
        {
            var db = new CompetenceDbContext();
            db.Add(test);
            db.SaveChanges();
            return (test);
        }

        public Test PatchTest(int id, JsonPatchDocument<Test> testModelJSON)
        {
            var db = new CompetenceDbContext();
            Test d = new Test();

            d = db.Test.FirstOrDefault(x => x.TestId == id);
            testModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public Test PutTest(Test test)
        {
            var db = new CompetenceDbContext();
            Test d = new Test();
            try
            {
                d = db.Test.First(x => x.TestId == test.TestId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, test.GetType().GetProperty(property.Name).GetValue(test));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(test);
                db.SaveChanges();
                d = test;
            }
            return d;
        }

        public void DeleteTest(int id)
        {
            var db = new CompetenceDbContext();
            Test d = new Test();
            d = this.GetTestById(id);
            db.Test.Remove(d);
            db.SaveChanges();
        }
    }
}


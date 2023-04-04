using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class TestComposeDAL
    {
        public List<TestCompose> GetAllTestCompose()
        {
            var db = new CompetenceDbContext();
            return db.TestCompose.ToList();
        }

        public TestCompose GetTestComposeById(int id)
        {
            var db = new CompetenceDbContext();
            TestCompose d = new TestCompose();

            d = db.TestCompose.FirstOrDefault(x => x.TestComposeId == id);

            return d;
        }


        public IEnumerable<TestCompose> GetTestComposeByTestId(int id)
        {
            var db = new CompetenceDbContext();
            List<TestCompose> d = new List<TestCompose>();

            d = db.TestCompose.Where(x => x.TestId == id).ToList();

            return d;
        }


        public TestCompose PostTestCompose(TestCompose testCompose)
        {
            var db = new CompetenceDbContext();
            db.Add(testCompose);
            db.SaveChanges();
            return (testCompose);
        }

        public TestCompose PatchTestCompose(int id, JsonPatchDocument<TestCompose> testComposeModelJSON)
        {
            var db = new CompetenceDbContext();
            TestCompose d = new TestCompose();

            d = db.TestCompose.FirstOrDefault(x => x.TestComposeId == id);
            testComposeModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public TestCompose PutTestCompose(TestCompose testCompose)
        {
            var db = new CompetenceDbContext();
            TestCompose d = new TestCompose();
            try
            {
                d = db.TestCompose.First(x => x.TestComposeId == testCompose.TestComposeId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, testCompose.GetType().GetProperty(property.Name).GetValue(testCompose));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(testCompose);
                db.SaveChanges();
                d = testCompose;
            }
            return d;
        }

        public void DeleteTestCompose(int id)
        {
            var db = new CompetenceDbContext();
            TestCompose d = new TestCompose();
            d = this.GetTestComposeById(id);
            db.TestCompose.Remove(d);
            db.SaveChanges();
        }
    }
}


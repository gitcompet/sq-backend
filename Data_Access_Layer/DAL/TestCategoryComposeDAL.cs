using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class TestCategoryComposeDAL
    {
        public List<TestCategoryCompose> GetAllTestCategoryCompose()
        {
            var db = new CompetenceDbContext();
            return db.TestCategoryCompose.ToList();
        }

        public TestCategoryCompose GetTestCategoryComposeById(int id)
        {
            var db = new CompetenceDbContext();
            TestCategoryCompose d = new TestCategoryCompose();

            d = db.TestCategoryCompose.FirstOrDefault(x => x.TestCategoryComposeId == id);

            return d;
        }
        public IEnumerable<TestCategoryCompose> GetTestUserByUserId(int id)
        {
            var db = new CompetenceDbContext();
            var d = db.TestCategoryCompose.Where(x => x.TestId == id);

            return d;
        }


        public TestCategoryCompose PostTestCategoryCompose(TestCategoryCompose testCategoryCompose)
        {
            var db = new CompetenceDbContext();
            db.Add(testCategoryCompose);
            db.SaveChanges();
            return (testCategoryCompose);
        }

        public TestCategoryCompose PatchTestCategoryCompose(int id, JsonPatchDocument<TestCategoryCompose> testCategoryComposeModelJSON)
        {
            var db = new CompetenceDbContext();
            TestCategoryCompose d = new TestCategoryCompose();

            d = db.TestCategoryCompose.FirstOrDefault(x => x.TestCategoryComposeId == id);
            testCategoryComposeModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public TestCategoryCompose PutTestCategoryCompose(TestCategoryCompose testCategoryCompose)
        {
            var db = new CompetenceDbContext();
            TestCategoryCompose d = new TestCategoryCompose();
            try
            {
                d = db.TestCategoryCompose.First(x => x.TestCategoryComposeId == testCategoryCompose.TestCategoryComposeId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, testCategoryCompose.GetType().GetProperty(property.Name).GetValue(testCategoryCompose));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(testCategoryCompose);
                db.SaveChanges();
                d = testCategoryCompose;
            }
            return d;
        }

        public void DeleteTestCategoryCompose(int id)
        {
            var db = new CompetenceDbContext();
            TestCategoryCompose d = new TestCategoryCompose();
            d = this.GetTestCategoryComposeById(id);
            db.TestCategoryCompose.Remove(d);
            db.SaveChanges();
        }
    }
}


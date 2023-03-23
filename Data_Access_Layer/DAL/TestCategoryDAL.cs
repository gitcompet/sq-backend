using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class TestCategoryDAL
    {
        public List<TestCategory> GetAllTestCategory()
        {
            var db = new CompetenceDbContext();
            return db.TestCategory.ToList();
        }

        public TestCategory GetTestCategoryById(int id)
        {
            var db = new CompetenceDbContext();
            TestCategory d = new TestCategory();

            d = db.TestCategory.FirstOrDefault(x => x.TestCategoryId == id);

            return d;
        }


        public TestCategory PostTestCategory(TestCategory testCategory)
        {
            var db = new CompetenceDbContext();
            db.Add(testCategory);
            db.SaveChanges();
            return (testCategory);
        }

        public TestCategory PatchTestCategory(int id, JsonPatchDocument<TestCategory> testCategoryModelJSON)
        {
            var db = new CompetenceDbContext();
            TestCategory d = new TestCategory();

            d = db.TestCategory.FirstOrDefault(x => x.TestCategoryId == id);
            testCategoryModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public TestCategory PutTestCategory(TestCategory testCategory)
        {
            var db = new CompetenceDbContext();
            TestCategory d = new TestCategory();
            try
            {
                d = db.TestCategory.First(x => x.TestCategoryId == testCategory.TestCategoryId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, testCategory.GetType().GetProperty(property.Name).GetValue(testCategory));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(testCategory);
                db.SaveChanges();
                d = testCategory;
            }
            return d;
        }

        public void DeleteTestCategory(int id)
        {
            var db = new CompetenceDbContext();
            TestCategory d = new TestCategory();
            d = this.GetTestCategoryById(id);
            db.TestCategory.Remove(d);
            db.SaveChanges();
        }
    }
}


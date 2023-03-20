using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class TestAttributionDAL
    {
        public List<TestAttribution> GetAllTestAttribution()
        {
            var db = new CompetenceDbContext();
            return db.TestAttribution.ToList();
        }

        public TestAttribution GetTestAttributionById(int id)
        {
            var db = new CompetenceDbContext();
            TestAttribution d = new TestAttribution();

            d = db.TestAttribution.FirstOrDefault(x => x.TestAttributionId == id);

            return d;
        }


        public TestAttribution PostTestAttribution(TestAttribution testAttribution)
        {
            var db = new CompetenceDbContext();
            db.Add(testAttribution);
            db.SaveChanges();
            return (testAttribution);
        }

        public TestAttribution PatchTestAttribution(int id, JsonPatchDocument<TestAttribution> testAttributionModelJSON)
        {
            var db = new CompetenceDbContext();
            TestAttribution d = new TestAttribution();

            d = db.TestAttribution.FirstOrDefault(x => x.TestAttributionId == id);
            testAttributionModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public TestAttribution PutTestAttribution(TestAttribution testAttribution)
        {
            var db = new CompetenceDbContext();
            TestAttribution d = new TestAttribution();
            try
            {
                d = db.TestAttribution.First(x => x.TestAttributionId == testAttribution.TestAttributionId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, testAttribution.GetType().GetProperty(property.Name).GetValue(testAttribution));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(testAttribution);
                db.SaveChanges();
                d = testAttribution;
            }
            return d;
        }

        public void DeleteTestAttribution(int id)
        {
            var db = new CompetenceDbContext();
            TestAttribution d = new TestAttribution();
            d = this.GetTestAttributionById(id);
            db.TestAttribution.Remove(d);
            db.SaveChanges();
        }
    }
}


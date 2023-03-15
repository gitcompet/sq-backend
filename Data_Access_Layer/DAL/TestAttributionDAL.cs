using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data_Access_Layer
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


        public void postTestAttribution(TestAttribution testattribution)
        {
            var db = new CompetenceDbContext();
            db.Add(testattribution);
            db.SaveChanges();
        }

    }
}


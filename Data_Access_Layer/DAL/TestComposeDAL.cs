using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


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


        public void postTestCompose(TestCompose testcompose)
        {
            var db = new CompetenceDbContext();
            db.Add(testcompose);
            db.SaveChanges();
        }

    }
}


using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data_Access_Layer.DAL
{
    public class TestStatusDAL
    {
        public List<TestStatus> GetAllTestStatus()
        {
            var db = new CompetenceDbContext();
            return db.TestStatus.ToList();
        }

        public TestStatus GetTestStatusById(int id)
        {
            var db = new CompetenceDbContext();
            TestStatus d = new TestStatus();

            d = db.TestStatus.FirstOrDefault(x => x.TestStatusId == id);

            return d;
        }


        public void postTestStatus(TestStatus teststatus)
        {
            var db = new CompetenceDbContext();
            db.Add(teststatus);
            db.SaveChanges();
        }

    }
}


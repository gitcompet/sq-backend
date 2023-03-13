using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


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


        public void postTest(Test test)
        {
            var db = new CompetenceDbContext();
            db.Add(test);
            db.SaveChanges();
        }

    }
}
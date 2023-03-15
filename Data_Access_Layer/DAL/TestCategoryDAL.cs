using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


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


        public void postTestCategory(TestCategory testcategory)
        {
            var db = new CompetenceDbContext();
            db.Add(testcategory);
            db.SaveChanges();
        }

    }
}
using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data_Access_Layer.DAL
{
    public class AnswerCandidateTestDAL
    {
        public List<AnswerCandidateTest> GetAllAnswerCandidateTest()
        {
            var db = new CompetenceDbContext();
            return db.AnswerCandidateTest.ToList();
        }

        public AnswerCandidateTest GetAnswerCandidateTestById(int id)
        {
            var db = new CompetenceDbContext();
            AnswerCandidateTest d = new AnswerCandidateTest();

            d = db.AnswerCandidateTest.FirstOrDefault(x => x.AssignmentTestId == id);

            return d;
        }


        public void postAnswerCandidateTest(AnswerCandidateTest answercandidatetest)
        {
            var db = new CompetenceDbContext();
            db.Add(answercandidatetest);
            db.SaveChanges();
        }

    }
}


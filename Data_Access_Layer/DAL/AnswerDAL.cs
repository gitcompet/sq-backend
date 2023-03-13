using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data_Access_Layer
{
    public class AnswerDAL
    {
        public List<Answer> GetAllAnswer()
        {
            var db = new CompetenceDbContext();
            return db.Answer.ToList();
        }

        public Answer GetAnswerById(int id)
        {
            var db = new CompetenceDbContext();
            Answer d = new Answer();

            d = db.Answer.FirstOrDefault(x => x.AnswerId == id);

            return d;
        }


        public void postAnswer(Answer answer)
        {
            var db = new CompetenceDbContext();
            db.Add(answer);
            db.SaveChanges();
        }

    }
}


using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data_Access_Layer.DAL
{
    public class QuizComposeDAL
    {
        public List<QuizCompose> GetAllQuizCompose()
        {
            var db = new CompetenceDbContext();
            return db.QuizCompose.ToList();
        }

        public QuizCompose GetQuizComposeById(int id)
        {
            var db = new CompetenceDbContext();
            QuizCompose d = new QuizCompose();

            d = db.QuizCompose.FirstOrDefault(x => x.TestComposeId == id);

            return d;
        }


        public void postQuizCompose(QuizCompose quizcompose)
        {
            var db = new CompetenceDbContext();
            db.Add(quizcompose);
            db.SaveChanges();
        }

    }
}


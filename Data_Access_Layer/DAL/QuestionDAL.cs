using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data_Access_Layer.DAL
{
    public class QuestionDAL
    {
        public List<Question> GetAllQuestion()
        {
            var db = new CompetenceDbContext();
            return db.Question.ToList();
        }

        public Question GetQuestionById(int id)
        {
            var db = new CompetenceDbContext();
            Question d = new Question();

            d = db.Question.FirstOrDefault(x => x.QuestionId == id);

            return d;
        }


        public void postQuestion(Question question)
        {
            var db = new CompetenceDbContext();
            db.Add(question);
            db.SaveChanges();
        }

    }
}


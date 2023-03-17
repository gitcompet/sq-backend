using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class QuizDAL
    {
        public List<Quiz> GetAllQuiz()
        {
            var db = new CompetenceDbContext();
            return db.Quiz.ToList();
        }

        public Quiz GetQuizById(int id)
        {
            var db = new CompetenceDbContext();
            Quiz d = new Quiz();

            d = db.Quiz.FirstOrDefault(x => x.QuizId == id);

            return d;
        }


        public Quiz PostQuiz(Quiz quiz)
        {
            var db = new CompetenceDbContext();
            db.Add(quiz);
            db.SaveChanges();
            return (quiz);
        }

        public Quiz PatchQuiz(int id, JsonPatchDocument<Quiz> quizModelJSON)
        {
            var db = new CompetenceDbContext();
            Quiz d = new Quiz();

            d = db.Quiz.FirstOrDefault(x => x.QuizId == id);
            quizModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public Quiz PutQuiz(Quiz quiz)
        {
            var db = new CompetenceDbContext();
            Quiz d = new Quiz();
            try
            {
                d = db.Quiz.First(x => x.QuizId == quiz.QuizId);
                foreach(PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, quiz.GetType().GetProperty(property.Name).GetValue(quiz));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(quiz);
                db.SaveChanges();
                d = quiz;
            }
            return d;
        }

        public void DeleteQuiz(int id)
        { 
            var db = new CompetenceDbContext();
            Quiz d = new Quiz();
            d = this.GetQuizById(id);
            db.Quiz.Remove(d);
            db.SaveChanges();
        }
    }
}


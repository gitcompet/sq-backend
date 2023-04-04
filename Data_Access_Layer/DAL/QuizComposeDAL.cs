using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

            d = db.QuizCompose.FirstOrDefault(x => x.QuizComposeId == id);

            return d;
        }


        public QuizCompose PostQuizCompose(QuizCompose quizCompose)
        {
            var db = new CompetenceDbContext();
            db.Add(quizCompose);
            db.SaveChanges();
            return (quizCompose);
        }

        public QuizCompose PatchQuizCompose(int id, JsonPatchDocument<QuizCompose> quizComposeModelJSON)
        {
            var db = new CompetenceDbContext();
            QuizCompose d = new QuizCompose();

            d = db.QuizCompose.FirstOrDefault(x => x.QuizComposeId == id);
            quizComposeModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public QuizCompose PutQuizCompose(QuizCompose quizCompose)
        {
            var db = new CompetenceDbContext();
            QuizCompose d = new QuizCompose();
            try
            {
                d = db.QuizCompose.First(x => x.QuizComposeId == quizCompose.QuizComposeId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, quizCompose.GetType().GetProperty(property.Name).GetValue(quizCompose));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(quizCompose);
                db.SaveChanges();
                d = quizCompose;
            }
            return d;
        }

        public void DeleteQuizCompose(int id)
        {
            var db = new CompetenceDbContext();
            QuizCompose d = new QuizCompose();
            d = this.GetQuizComposeById(id);
            db.QuizCompose.Remove(d);
            db.SaveChanges();
        }
    }
}


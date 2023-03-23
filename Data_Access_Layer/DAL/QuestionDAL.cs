using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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


        public Question PostQuestion(Question question)
        {
            var db = new CompetenceDbContext();
            db.Add(question);
            db.SaveChanges();
            return (question);
        }

        public Question PatchQuestion(int id, JsonPatchDocument<Question> questionModelJSON)
        {
            var db = new CompetenceDbContext();
            Question d = new Question();

            d = db.Question.FirstOrDefault(x => x.QuestionId == id);
            questionModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public Question PutQuestion(Question question)
        {
            var db = new CompetenceDbContext();
            Question d = new Question();
            try
            {
                d = db.Question.First(x => x.QuestionId == question.QuestionId);
                foreach(PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, question.GetType().GetProperty(property.Name).GetValue(question));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(question);
                db.SaveChanges();
                d = question;
            }
            return d;
        }

        public void DeleteQuestion(int id)
        { 
            var db = new CompetenceDbContext();
            Question d = new Question();
            d = this.GetQuestionById(id);
            db.Question.Remove(d);
            db.SaveChanges();
        }
    }
}


using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
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


        public Answer PostAnswer(Answer answer)
        {
            var db = new CompetenceDbContext();
            db.Add(answer);
            db.SaveChanges();
            return (answer);
        }

        public Answer PatchAnswer(int id, JsonPatchDocument<Answer> answerModelJSON)
        {
            var db = new CompetenceDbContext();
            Answer d = new Answer();

            d = db.Answer.FirstOrDefault(x => x.AnswerId == id);
            answerModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public Answer PutAnswer(Answer answer)
        {
            var db = new CompetenceDbContext();
            Answer d = new Answer();
            try
            {
                d = db.Answer.First(x => x.AnswerId == answer.AnswerId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, answer.GetType().GetProperty(property.Name).GetValue(answer));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(answer);
                db.SaveChanges();
                d = answer;
            }
            return d;
        }

        public void DeleteAnswer(int id)
        {
            var db = new CompetenceDbContext();
            Answer d = new Answer();
            d = this.GetAnswerById(id);
            db.Answer.Remove(d);
            db.SaveChanges();
        }
    }
}


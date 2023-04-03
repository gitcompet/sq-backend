using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class AnswerQuestionDAL
    {
        public List<AnswerQuestion> GetAllAnswerQuestion()
        {
            var db = new CompetenceDbContext();
            return db.AnswerQuestion.ToList();
        }

        public AnswerQuestion GetAnswerQuestionById(int id)
        {
            var db = new CompetenceDbContext();
            AnswerQuestion d = new AnswerQuestion();

            d = db.AnswerQuestion.FirstOrDefault(x => x.AnswerQuestionId == id);

            return d;
        }
        public IEnumerable<AnswerQuestion> GetAnswerQuestionByQuestionId(int id)
        {
            var db = new CompetenceDbContext();
            var d = db.AnswerQuestion.Where(x => x.QuestionId == id);

            return d;
        }

        public AnswerQuestion PostAnswerQuestion(AnswerQuestion answerQuestion)
        {
            var db = new CompetenceDbContext();
            db.Add(answerQuestion);
            db.SaveChanges();
            return (answerQuestion);
        }

        public AnswerQuestion PatchAnswerQuestion(int id, JsonPatchDocument<AnswerQuestion> answerQuestionModelJSON)
        {
            var db = new CompetenceDbContext();
            AnswerQuestion d = new AnswerQuestion();

            d = db.AnswerQuestion.FirstOrDefault(x => x.AnswerQuestionId == id);
            answerQuestionModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public AnswerQuestion PutAnswerQuestion(AnswerQuestion answerQuestion)
        {
            var db = new CompetenceDbContext();
            AnswerQuestion d = new AnswerQuestion();
            try
            {
                d = db.AnswerQuestion.First(x => x.AnswerQuestionId == answerQuestion.AnswerQuestionId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, answerQuestion.GetType().GetProperty(property.Name).GetValue(answerQuestion));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(answerQuestion);
                db.SaveChanges();
                d = answerQuestion;
            }
            return d;
        }

        public void DeleteAnswerQuestion(int id)
        {
            var db = new CompetenceDbContext();
            AnswerQuestion d = new AnswerQuestion();
            d = this.GetAnswerQuestionById(id);
            db.AnswerQuestion.Remove(d);
            db.SaveChanges();
        }
    }
}


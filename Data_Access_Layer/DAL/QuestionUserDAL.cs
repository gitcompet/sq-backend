using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class QuestionUserDAL
    {
        public List<QuestionUser> GetAllQuestionUser()
        {
            var db = new CompetenceDbContext();
            return db.QuestionUser.ToList();
        }

        public QuestionUser GetQuestionUserById(int id)
        {
            var db = new CompetenceDbContext();
            QuestionUser d = new QuestionUser();

            d = db.QuestionUser.FirstOrDefault(x => x.QuestionUserId == id);

            return d;
        }
        public QuestionUser GetQuestionUserHiddenById(int id)
        {
            var db = new CompetenceDbContext();
            QuestionUser d = new QuestionUser();

            d = db.QuestionUser.FirstOrDefault(x => x.QuestionUserId == id);

            return d;
        }

        public IEnumerable<QuestionUser> GetQuestionUserByLinkId(int id)
        {

            var db = new CompetenceDbContext();
            var d = db.QuestionUser.Where(x => x.QuizUserId == id);

            return d;
        }


        public QuestionUser PostQuestionUser(QuestionUser questionUser)
        {
            var db = new CompetenceDbContext();
            db.Add(questionUser);
            db.SaveChanges();
            return (questionUser);
        }

        public QuestionUser PatchQuestionUser(int id, JsonPatchDocument<QuestionUser> questionUserModelJSON)
        {
            var db = new CompetenceDbContext();
            QuestionUser d = new QuestionUser();

            d = db.QuestionUser.FirstOrDefault(x => x.QuestionUserId == id);
            questionUserModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }
        public bool PatchQuestionUserHidden(int id, DateTime maxValidationDate)
        {
            var db = new CompetenceDbContext();
            QuestionUser d = new QuestionUser();

            d = db.QuestionUser.FirstOrDefault(x => x.QuestionUserId == id);
            if (d != null)
            {
                d.MaxValidationDate = maxValidationDate;
                db.Update(d);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public QuestionUser PutQuestionUser(QuestionUser questionUser)
        {
            var db = new CompetenceDbContext();
            QuestionUser d = new QuestionUser();
            try
            {
                d = db.QuestionUser.First(x => x.QuestionUserId == questionUser.QuestionUserId);
                foreach(PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, questionUser.GetType().GetProperty(property.Name).GetValue(questionUser));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(questionUser);
                db.SaveChanges();
                d = questionUser;
            }
            return d;
        }

        public void DeleteQuestionUser(int id)
        { 
            var db = new CompetenceDbContext();
            QuestionUser d = new QuestionUser();
            d = this.GetQuestionUserById(id);
            db.QuestionUser.Remove(d);
            db.SaveChanges();
        }
    }
}


using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using Org.BouncyCastle.Crypto.Encodings;
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
            var localTimezone = TimeZoneInfo.Local.Id.ToString();
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(localTimezone);
            if (d.MaxValidationDate.HasValue)
            {
                d.MaxValidationDate = TimeZoneInfo.ConvertTimeFromUtc(d.MaxValidationDate.Value, timezone);
            }
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
            var d = db.QuestionUser.Where(x => x.QuizUserId == id && (x.MaxValidationDate == null || x.MaxValidationDate > DateTime.Now)).ToList();

            var localTimezone = TimeZoneInfo.Local.Id.ToString();
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(localTimezone);
            foreach (var item in d)
            {
                if (item.MaxValidationDate.HasValue)
                {
                    item.MaxValidationDate = TimeZoneInfo.ConvertTimeFromUtc(item.MaxValidationDate.Value, timezone);
                }
            }
            var d1 = new List<QuestionUser>();
            
            foreach (var item in d)
            {
                if (item.MaxValidationDate == null || item.MaxValidationDate > DateTime.Now)
                {
                    d1.Add(item);
                }
            }

            return d1;
        }
        public IEnumerable<QuestionUser> GetQuestionUserByLinkId(int id, bool scoring)
        {
            var db = new CompetenceDbContext();
            var d = db.QuestionUser.Where(x => x.QuizUserId == id).ToList();

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


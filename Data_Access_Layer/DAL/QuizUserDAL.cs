using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class QuizUserDAL
    {
        public List<QuizUser> GetAllQuizUser()
        {
            var db = new CompetenceDbContext();
            return db.QuizUser.ToList();
        }

        public QuizUser GetQuizUserById(int id)
        {
            var db = new CompetenceDbContext();
            QuizUser d = new QuizUser();

            d = db.QuizUser.FirstOrDefault(x => x.QuizUserId == id);
            var localTimezone = TimeZoneInfo.Local.Id.ToString();
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(localTimezone);
            d.AuditTrailClosingTime = TimeZoneInfo.ConvertTimeFromUtc(d.AuditTrailClosingTime.Value, timezone);

            return d;
        }
        
        public IEnumerable<QuizUser> GetQuizUserByLinkId(int id)
        {

            var db = new CompetenceDbContext();
            var d = db.QuizUser.Where(x => x.TestUserId == id);
            var localTimezone = TimeZoneInfo.Local.Id.ToString();
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(localTimezone);
            foreach(var item in d)
            {
                item.AuditTrailClosingTime = TimeZoneInfo.ConvertTimeFromUtc(item.AuditTrailClosingTime.Value, timezone);
            }

            return d;
        }


        public QuizUser PostQuizUser(QuizUser quizUser)
        {
            var db = new CompetenceDbContext();
            db.Add(quizUser);
            db.SaveChanges();
            return (quizUser);
        }

        public QuizUser PatchQuizUser(int id, JsonPatchDocument<QuizUser> quizUserModelJSON)
        {
            var db = new CompetenceDbContext();
            QuizUser d = new QuizUser();

            d = db.QuizUser.FirstOrDefault(x => x.QuizUserId == id);
            quizUserModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public QuizUser PutQuizUser(QuizUser quizUser)
        {
            var db = new CompetenceDbContext();
            QuizUser d = new QuizUser();
            try
            {
                d = db.QuizUser.First(x => x.QuizUserId == quizUser.QuizUserId);
                foreach(PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, quizUser.GetType().GetProperty(property.Name).GetValue(quizUser));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(quizUser);
                db.SaveChanges();
                d = quizUser;
            }
            return d;
        }

        public void DeleteQuizUser(int id)
        { 
            var db = new CompetenceDbContext();
            QuizUser d = new QuizUser();
            d = this.GetQuizUserById(id);
            db.QuizUser.Remove(d);
            db.SaveChanges();
        }
        
        public List<QuizUser> GetLastQuizValidates(int qty)
        {
            var db = new CompetenceDbContext();
            return db.QuizUser.OrderByDescending(x => x.AuditTrailClosingTime).Take(qty).ToList();
        }
        public List<QuizUser> GetPendingQuizes(int qty)
        {
            var db = new CompetenceDbContext();
            return db.QuizUser.OrderByDescending(x => x.QuizUserId).Where(x => x.IsClosed == false).Take(qty).ToList();
        }
    }
}


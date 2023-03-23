using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class AnswerUserDAL
    {
        public List<AnswerUser> GetAllAnswerUser()
        {
            var db = new CompetenceDbContext();
            return db.AnswerUser.ToList();
        }

        public AnswerUser GetAnswerUserById(int id)
        {
            var db = new CompetenceDbContext();
            AnswerUser d = new AnswerUser();

            d = db.AnswerUser.FirstOrDefault(x => x.AnswerUserId == id);

            return d;
        }
        public IEnumerable<AnswerUser> GetAnswerUserByLinkId(int id)
        {

            var db = new CompetenceDbContext();
            var d = db.AnswerUser.Where(x => x.QuestionUserId == id);

            return d;
        }



        public AnswerUser PostAnswerUser(AnswerUser answerUser)
        {
            var db = new CompetenceDbContext();
            db.Add(answerUser);
            db.SaveChanges();
            return (answerUser);
        }

        public AnswerUser PatchAnswerUser(int id, JsonPatchDocument<AnswerUser> answerUserModelJSON)
        {
            var db = new CompetenceDbContext();
            AnswerUser d = new AnswerUser();

            d = db.AnswerUser.FirstOrDefault(x => x.AnswerUserId == id);
            answerUserModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public AnswerUser PutAnswerUser(AnswerUser answerUser)
        {
            var db = new CompetenceDbContext();
            AnswerUser d = new AnswerUser();
            try
            {
                d = db.AnswerUser.First(x => x.AnswerUserId == answerUser.AnswerUserId);
                foreach(PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, answerUser.GetType().GetProperty(property.Name).GetValue(answerUser));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(answerUser);
                db.SaveChanges();
                d = answerUser;
            }
            return d;
        }

        public void DeleteAnswerUser(int id)
        { 
            var db = new CompetenceDbContext();
            AnswerUser d = new AnswerUser();
            d = this.GetAnswerUserById(id);
            db.AnswerUser.Remove(d);
            db.SaveChanges();
        }
    }
}


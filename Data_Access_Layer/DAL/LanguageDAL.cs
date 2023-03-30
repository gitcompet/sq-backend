using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class LanguageDAL
    {
        public List<Language> GetAllLanguage()
        {
            var db = new CompetenceDbContext();
            return db.Language.ToList();
        }

        public Language GetLanguageById(int id)
        {
            var db = new CompetenceDbContext();
            Language d = new Language();

            d = db.Language.FirstOrDefault(x => x.LanguageId == id);

            return d;
        }


        public Language PostLanguage(Language language)
        {
            var db = new CompetenceDbContext();
            db.Add(language);
            db.SaveChanges();
            return (language);
        }

        public Language PatchLanguage(int id, JsonPatchDocument<Language> languageModelJSON)
        {
            var db = new CompetenceDbContext();
            Language d = new Language();

            d = db.Language.FirstOrDefault(x => x.LanguageId == id);
            languageModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public Language PutLanguage(Language language)
        {
            var db = new CompetenceDbContext();
            Language d = new Language();
            try
            {
                d = db.Language.First(x => x.LanguageId == language.LanguageId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, language.GetType().GetProperty(property.Name).GetValue(language));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(language);
                db.SaveChanges();
                d = language;
            }
            return d;
        }

        public void DeleteLanguage(int id)
        {
            var db = new CompetenceDbContext();
            Language d = new Language();
            d = this.GetLanguageById(id);
            db.Language.Remove(d);
            db.SaveChanges();
        }
    }
}


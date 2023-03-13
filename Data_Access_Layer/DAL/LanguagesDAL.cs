﻿using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data_Access_Layer.DAL
{
    public class LanguagesDAL
    {
        public List<Languages> GetAllLanguages()
        {
            var db = new CompetenceDbContext();
            return db.Languages.ToList();
        }

        public Languages GetLanguagesById(int id)
        {
            var db = new CompetenceDbContext();
            Languages d = new Languages();

            d = db.Languages.FirstOrDefault(x => x.LanguagesID == id);

            return d;
        }


        public int postLanguages(Languages languages)
        {
            var tempLanguage = new Languages();
            var db = new CompetenceDbContext();
            db.Add(languages);
            db.SaveChanges();
            return languages.LanguagesID;
        }

    }
}

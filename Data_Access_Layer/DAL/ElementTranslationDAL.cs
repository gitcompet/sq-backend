using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data_Access_Layer
{
    public class ElementTranslationDAL
    {
        public List<ElementTranslation> GetAllElementTranslation()
        {
            var db = new CompetenceDbContext();
            return db.ElementTranslation.ToList();
        }

        public ElementTranslation GetElementTranslationById(int id)
        {
            var db = new CompetenceDbContext();
            ElementTranslation d = new ElementTranslation();

            d = db.ElementTranslation.FirstOrDefault(x => x.ElementTranslationId == id);

            return d;
        }


        public void postElementTranslation(ElementTranslation elementtranslation)
        {
            var db = new CompetenceDbContext();
            db.Add(elementtranslation);
            db.SaveChanges();
        }

    }
}


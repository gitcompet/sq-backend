using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using MySqlX.XDevAPI.Common;
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

        //Get a list of libelle from a list of ID with the type looking for
        public IEnumerable<String> GetAnswerByListId(IEnumerable<String> ids, string elementType, int languageId)
        {
            var result = new List<String>();
            foreach (var id in ids)
            {
                var db = new CompetenceDbContext();
                ElementTranslation d = new ElementTranslation();

                d = db.ElementTranslation.FirstOrDefault(x => x.ElementType == elementType.ToString() && x.LanguagesId == languageId && x.ElementId == int.Parse(id));

                result.Add(d.Description);
            }
            return result;
        }
    }
}


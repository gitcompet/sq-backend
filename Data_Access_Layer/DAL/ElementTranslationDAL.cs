using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
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


        public ElementTranslation PostElementTranslation(ElementTranslation elementTranslation)
        {
            var db = new CompetenceDbContext();
            db.Add(elementTranslation);
            db.SaveChanges();
            return (elementTranslation);
        }

        public ElementTranslation PatchElementTranslation(int id, JsonPatchDocument<ElementTranslation> elementTranslationModelJSON)
        {
            var db = new CompetenceDbContext();
            ElementTranslation d = new ElementTranslation();

            d = db.ElementTranslation.FirstOrDefault(x => x.ElementTranslationId == id);
            elementTranslationModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public ElementTranslation PutElementTranslation(ElementTranslation elementTranslation)
        {
            var db = new CompetenceDbContext();
            ElementTranslation d = new ElementTranslation();
            try
            {
                d = db.ElementTranslation.First(x => x.ElementTranslationId == elementTranslation.ElementTranslationId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, elementTranslation.GetType().GetProperty(property.Name).GetValue(elementTranslation));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(elementTranslation);
                db.SaveChanges();
                d = elementTranslation;
            }
            return d;
        }

        public void DeleteElementTranslation(int id)
        {
            var db = new CompetenceDbContext();
            ElementTranslation d = new ElementTranslation();
            d = this.GetElementTranslationById(id);
            db.ElementTranslation.Remove(d);
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


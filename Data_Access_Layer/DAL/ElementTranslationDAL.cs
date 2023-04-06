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

        public IEnumerable<string> GetElementsLabelById(IEnumerable<string> ids, string elementType, int languageId)
        {
            var result = new List<string>();

            foreach (var id in ids)
            {
                result.Add(GetElementLabelById(id, elementType, languageId));
            }
            return result;
        }
        public string GetElementLabelById(string id, string elementType, int languageId)
        {
            string result;
            var db = new CompetenceDbContext();
            ElementTranslation d = new ElementTranslation();

            d = db.ElementTranslation.FirstOrDefault(x => x.ElementType == elementType && x.LanguagesId == languageId && x.ElementId == int.Parse(id));

            if (d == null)
            {
                //IF not found a label, we use english by default
                d = db.ElementTranslation.FirstOrDefault(x => x.ElementType == elementType && x.LanguagesId == 2 && x.ElementId == int.Parse(id));
                if (d == null)
                {

                    result = "Please contact an admin. Please give him these informations : " + elementType + " " + id;
                }
                else
                {
                    result = d.Description;
                }
            }
            else
            {
                result = d.Description;
            }
            return result;
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

        public void DeleteElementTranslationByItem(int id, string type)
        {
            var db = new CompetenceDbContext();
            List<ElementTranslation> d = new List<ElementTranslation>();

            d = db.ElementTranslation.Where(x => x.ElementId == id && x.ElementType == type).ToList();
            foreach (var item in d)
            {
                db.ElementTranslation.Remove(item);
            }
            db.SaveChanges();
        }

        //Get a list of label from a list of ID with the type looking for
        public IEnumerable<string> GetAnswerByListId(IEnumerable<string> ids, string elementType, int languageId)
        {
            var result = new List<string>();
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


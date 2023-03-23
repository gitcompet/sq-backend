using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class DomainComposeDAL
    {
        public List<DomainCompose> GetAllDomainCompose()
        {
            var db = new CompetenceDbContext();
            return db.DomainCompose.ToList();
        }

        public DomainCompose GetDomainComposeById(int id)
        {
            var db = new CompetenceDbContext();
            DomainCompose d = new DomainCompose();

            d = db.DomainCompose.FirstOrDefault(x => x.DomainComposeId == id);

            return d;
        }


        public DomainCompose PostDomainCompose(DomainCompose domainCompose)
        {
            var db = new CompetenceDbContext();
            db.Add(domainCompose);
            db.SaveChanges();
            return (domainCompose);
        }

        public DomainCompose PatchDomainCompose(int id, JsonPatchDocument<DomainCompose> domainComposeModelJSON)
        {
            var db = new CompetenceDbContext();
            DomainCompose d = new DomainCompose();

            d = db.DomainCompose.FirstOrDefault(x => x.DomainComposeId == id);
            domainComposeModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public DomainCompose PutDomainCompose(DomainCompose domainCompose)
        {
            var db = new CompetenceDbContext();
            DomainCompose d = new DomainCompose();
            try
            {
                d = db.DomainCompose.First(x => x.DomainComposeId == domainCompose.DomainComposeId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, domainCompose.GetType().GetProperty(property.Name).GetValue(domainCompose));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(domainCompose);
                db.SaveChanges();
                d = domainCompose;
            }
            return d;
        }

        public void DeleteDomainCompose(int id)
        {
            var db = new CompetenceDbContext();
            DomainCompose d = new DomainCompose();
            d = this.GetDomainComposeById(id);
            db.DomainCompose.Remove(d);
            db.SaveChanges();
        }
    }
}


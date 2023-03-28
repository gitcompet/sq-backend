using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class SubDomainComposeDAL
    {
        public List<SubDomainCompose> GetAllSubDomainCompose()
        {
            var db = new CompetenceDbContext();
            return db.SubDomainCompose.ToList();
        }

        public SubDomainCompose GetSubDomainComposeById(int id)
        {
            var db = new CompetenceDbContext();
            SubDomainCompose d = new SubDomainCompose();

            d = db.SubDomainCompose.FirstOrDefault(x => x.SubDomainComposeId == id);

            return d;
        }
        public IEnumerable<SubDomainCompose> GetSubDomainComposeByElementId(string type, int id)
        {
            var db = new CompetenceDbContext();
            var d = db.SubDomainCompose.Where(x => x.ElementType == type && x.ElementId == id);

            return d;
        }


        public SubDomainCompose PostSubDomainCompose(SubDomainCompose subDomainCompose)
        {
            var db = new CompetenceDbContext();
            db.Add(subDomainCompose);
            db.SaveChanges();
            return (subDomainCompose);
        }

        public SubDomainCompose PatchSubDomainCompose(int id, JsonPatchDocument<SubDomainCompose> subDomainComposeModelJSON)
        {
            var db = new CompetenceDbContext();
            SubDomainCompose d = new SubDomainCompose();

            d = db.SubDomainCompose.FirstOrDefault(x => x.SubDomainComposeId == id);
            subDomainComposeModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public SubDomainCompose PutSubDomainCompose(SubDomainCompose subDomainCompose)
        {
            var db = new CompetenceDbContext();
            SubDomainCompose d = new SubDomainCompose();
            try
            {
                d = db.SubDomainCompose.First(x => x.SubDomainComposeId == subDomainCompose.SubDomainComposeId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, subDomainCompose.GetType().GetProperty(property.Name).GetValue(subDomainCompose));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(subDomainCompose);
                db.SaveChanges();
                d = subDomainCompose;
            }
            return d;
        }

        public void DeleteSubDomainCompose(int id)
        {
            var db = new CompetenceDbContext();
            SubDomainCompose d = new SubDomainCompose();
            d = this.GetSubDomainComposeById(id);
            db.SubDomainCompose.Remove(d);
            db.SaveChanges();
        }
    }
}


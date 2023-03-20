using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class SubDomainDAL
    {
        public List<SubDomain> GetAllSubDomain()
        {
            var db = new CompetenceDbContext();
            return db.SubDomain.ToList();
        }

        public SubDomain GetSubDomainById(int id)
        {
            var db = new CompetenceDbContext();
            SubDomain d = new SubDomain();

            d = db.SubDomain.FirstOrDefault(x => x.SubDomainId == id);

            return d;
        }


        public SubDomain PostSubDomain(SubDomain subDomain)
        {
            var db = new CompetenceDbContext();
            db.Add(subDomain);
            db.SaveChanges();
            return (subDomain);
        }

        public SubDomain PatchSubDomain(int id, JsonPatchDocument<SubDomain> subDomainModelJSON)
        {
            var db = new CompetenceDbContext();
            SubDomain d = new SubDomain();

            d = db.SubDomain.FirstOrDefault(x => x.SubDomainId == id);
            subDomainModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public SubDomain PutSubDomain(SubDomain subDomain)
        {
            var db = new CompetenceDbContext();
            SubDomain d = new SubDomain();
            try
            {
                d = db.SubDomain.First(x => x.SubDomainId == subDomain.SubDomainId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, subDomain.GetType().GetProperty(property.Name).GetValue(subDomain));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(subDomain);
                db.SaveChanges();
                d = subDomain;
            }
            return d;
        }

        public void DeleteSubDomain(int id)
        {
            var db = new CompetenceDbContext();
            SubDomain d = new SubDomain();
            d = this.GetSubDomainById(id);
            db.SubDomain.Remove(d);
            db.SaveChanges();
        }
    }
}


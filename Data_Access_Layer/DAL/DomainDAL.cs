using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class DomainDAL
    {
        public List<Domain> GetAllDomain()
        {
            var db = new CompetenceDbContext();
            return db.Domain.ToList();
        }

        public Domain GetDomainById(int id)
        {
            var db = new CompetenceDbContext();
            Domain d = new Domain();

            d = db.Domain.FirstOrDefault(x => x.DomainId == id);

            return d;
        }


        public Domain PostDomain(Domain domain)
        {
            var db = new CompetenceDbContext();
            db.Add(domain);
            db.SaveChanges();
            return (domain);
        }

        public Domain PatchDomain(int id, JsonPatchDocument<Domain> domainModelJSON)
        {
            var db = new CompetenceDbContext();
            Domain d = new Domain();

            d = db.Domain.FirstOrDefault(x => x.DomainId == id);
            domainModelJSON.ApplyTo(d);
            db.Update(d);
            db.SaveChanges();
            return d;
        }

        public Domain PutDomain(Domain domain)
        {
            var db = new CompetenceDbContext();
            Domain d = new Domain();
            try
            {
                d = db.Domain.First(x => x.DomainId == domain.DomainId);
                foreach (PropertyInfo property in d.GetType().GetProperties())
                {
                    d.GetType().GetProperty(property.Name).SetValue(d, domain.GetType().GetProperty(property.Name).GetValue(domain));
                }
                db.SaveChanges();
            }
            catch
            {
                db.Add(domain);
                db.SaveChanges();
                d = domain;
            }
            return d;
        }

        public void DeleteDomain(int id)
        {
            var db = new CompetenceDbContext();
            Domain d = new Domain();
            d = this.GetDomainById(id);
            db.Domain.Remove(d);
            db.SaveChanges();
        }
    }
}


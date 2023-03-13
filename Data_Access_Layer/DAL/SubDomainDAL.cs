using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


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


        public void postSubDomain(SubDomain subdomain)
        {
            var db = new CompetenceDbContext();
            db.Add(subdomain);
            db.SaveChanges();
        }

    }
}


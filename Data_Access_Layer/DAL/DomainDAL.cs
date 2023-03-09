using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


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


        public void postDomain(Domain domain)
        {
            var db = new CompetenceDbContext();
            db.Add(domain);
            db.SaveChanges();
        }

    }
}


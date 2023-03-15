using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


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


        public void postDomainCompose(DomainCompose domaincompose)
        {
            var db = new CompetenceDbContext();
            db.Add(domaincompose);
            db.SaveChanges();
        }

    }
}


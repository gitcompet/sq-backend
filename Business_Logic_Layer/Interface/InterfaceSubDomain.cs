using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceSubDomain
    {

        List<SubDomainModel> GetAllSubDomain();


        SubDomainModel GetSubDomainById(int id);
        void PostSubDomain(SubDomainModel subdomainModel);
    }
}

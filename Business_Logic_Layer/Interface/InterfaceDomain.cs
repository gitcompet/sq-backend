using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceDomain
    {

        List<DomainModel> GetAllDomain();


        DomainModel GetDomainById(int id);
        void PostDomain(DomainModel domainModel);
    }
}

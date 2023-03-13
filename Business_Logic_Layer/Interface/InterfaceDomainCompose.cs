using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceDomainCompose
    {

        List<DomainComposeModel> GetAllDomainCompose();


        DomainComposeModel GetDomainComposeById(int id);
        void PostDomainCompose(DomainComposeModel domaincomposeModel);
    }
}

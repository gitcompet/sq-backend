using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceDomain
    {

        List<DomainModel> GetAllDomain();
        DomainModel GetDomainById(int id);
        DomainModel PostDomain(DomainModel domainModel);
        DomainModel PatchDomain(int id, JsonPatchDocument<Domain> domainModelJSON);
        DomainModel PutDomain(DomainModel domainModel);
        void DeleteDomain(int id);
    }
}

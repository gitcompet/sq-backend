using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceSubDomain
    {

        List<SubDomainModel> GetAllSubDomain();
        SubDomainModel GetSubDomainById(int id);
        SubDomainModel PostSubDomain(SubDomainModel subDomainModel);
        SubDomainModel PatchSubDomain(int id, JsonPatchDocument<SubDomain> subDomainModelJSON);
        SubDomainModel PutSubDomain(SubDomainModel subDomainModel);
        void DeleteSubDomain(int id);
    }
}

using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceDomainCompose
    {

        List<DomainComposeModel> GetAllDomainCompose();
        DomainComposeModel GetDomainComposeById(int id);
        DomainComposeModel PostDomainCompose(DomainComposeModel domainComposeModel);
        DomainComposeModel PatchDomainCompose(int id, JsonPatchDocument<DomainCompose> domainComposeModelJSON);
        DomainComposeModel PutDomainCompose(DomainComposeModel domainComposeModel);
        IEnumerable<DomainComposeModel> GetDomainComposeByElementId(string type, int id);
        void DeleteDomainCompose(int id);
    }
}

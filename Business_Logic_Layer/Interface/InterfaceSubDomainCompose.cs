using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceSubDomainCompose
    {

        List<SubDomainComposeModel> GetAllSubDomainCompose();
        SubDomainComposeModel GetSubDomainComposeById(int id);
        SubDomainComposeModel PostSubDomainCompose(SubDomainComposeModel subDomainComposeModel);
        SubDomainComposeModel PatchSubDomainCompose(int id, JsonPatchDocument<SubDomainCompose> subDomainComposeModelJSON);
        SubDomainComposeModel PutSubDomainCompose(SubDomainComposeModel subDomainComposeModel);
        IEnumerable<SubDomainComposeModel> GetSubDomainComposeByElementId(string type, int id);
        void DeleteSubDomainCompose(int id);
    }
}

using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceTestAttribution
    {

        List<TestAttributionModel> GetAllTestAttribution();
        TestAttributionModel GetTestAttributionById(int id);
        TestAttributionModel PostTestAttribution(TestAttributionModel testAttributionModel);
        TestAttributionModel PatchTestAttribution(int id, JsonPatchDocument<TestAttribution> testAttributionModelJSON);
        TestAttributionModel PutTestAttribution(TestAttributionModel testAttributionModel);
        void DeleteTestAttribution(int id);
    }
}

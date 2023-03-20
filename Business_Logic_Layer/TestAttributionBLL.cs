using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;
using Microsoft.AspNetCore.JsonPatch;

namespace Business_Logic_Layer
{
    public class TestAttributionBLL : InterfaceTestAttribution
    {

        private TestAttributionDAL _DAL;
        private Mapper _TestAttributionMapper;

        public TestAttributionBLL()
        {
            _DAL = new Data_Access_Layer.DAL.TestAttributionDAL();
            var _configTestAttribution = new MapperConfiguration(cfg => cfg.CreateMap<TestAttribution, TestAttributionModel>().ReverseMap());

            _TestAttributionMapper = new Mapper(_configTestAttribution);
        }

        public List<TestAttributionModel> GetAllTestAttribution()
        {
            List<TestAttribution> testAttributionFromDB = _DAL.GetAllTestAttribution();
            List<TestAttributionModel> testAttributionModel = _TestAttributionMapper.Map<List<TestAttribution>, List<TestAttributionModel>>(testAttributionFromDB);

            return testAttributionModel;
        }

        public TestAttributionModel GetTestAttributionById(int id)
        {
            var testAttributionEntity = _DAL.GetTestAttributionById(id);

            TestAttributionModel testAttributionModel = _TestAttributionMapper.Map<TestAttribution, TestAttributionModel>(testAttributionEntity);

            return testAttributionModel;
        }


        public TestAttributionModel PostTestAttribution(TestAttributionModel testAttributionModel)
        {
            TestAttribution testAttributionEntity = _TestAttributionMapper.Map<TestAttributionModel, TestAttribution>(testAttributionModel);
            var testAttribution = _DAL.PostTestAttribution(testAttributionEntity);
            TestAttributionModel testAttributionModelReturn = _TestAttributionMapper.Map<TestAttribution, TestAttributionModel>(testAttribution);
            return testAttributionModelReturn;
        }


        public TestAttributionModel PatchTestAttribution(int id, JsonPatchDocument<TestAttribution> testAttributionModelJSON)
        {
            var testAttributionEntity = _DAL.PatchTestAttribution(id, testAttributionModelJSON);

            TestAttributionModel testAttributionModel = _TestAttributionMapper.Map<TestAttribution, TestAttributionModel>(testAttributionEntity);

            return testAttributionModel;
        }

        public TestAttributionModel PutTestAttribution(TestAttributionModel testAttributionModel)
        {
            TestAttribution testAttributionEntity = _TestAttributionMapper.Map<TestAttributionModel, TestAttribution>(testAttributionModel);
            var testAttribution = _DAL.PutTestAttribution(testAttributionEntity);
            TestAttributionModel testAttributionModelReturn = _TestAttributionMapper.Map<TestAttribution, TestAttributionModel>(testAttribution);
            return testAttributionModelReturn;
        }
        public void DeleteTestAttribution(int id)
        {
            _DAL.DeleteTestAttribution(id);
        }
    }
}


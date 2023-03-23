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
    public class TestComposeBLL : InterfaceTestCompose
    {

        private TestComposeDAL _DAL;
        private Mapper _TestComposeMapper;

        public TestComposeBLL()
        {
            _DAL = new Data_Access_Layer.DAL.TestComposeDAL();
            var _configTestCompose = new MapperConfiguration(cfg => cfg.CreateMap<TestCompose, TestComposeModel>().ReverseMap());

            _TestComposeMapper = new Mapper(_configTestCompose);
        }

        public List<TestComposeModel> GetAllTestCompose()
        {
            List<TestCompose> testComposeFromDB = _DAL.GetAllTestCompose();
            List<TestComposeModel> testComposeModel = _TestComposeMapper.Map<List<TestCompose>, List<TestComposeModel>>(testComposeFromDB);

            return testComposeModel;
        }

        public TestComposeModel GetTestComposeById(int id)
        {
            var testComposeEntity = _DAL.GetTestComposeById(id);

            TestComposeModel testComposeModel = _TestComposeMapper.Map<TestCompose, TestComposeModel>(testComposeEntity);

            return testComposeModel;
        }


        public TestComposeModel PostTestCompose(TestComposeModel testComposeModel)
        {
            TestCompose testComposeEntity = _TestComposeMapper.Map<TestComposeModel, TestCompose>(testComposeModel);
            var testCompose = _DAL.PostTestCompose(testComposeEntity);
            TestComposeModel testComposeModelReturn = _TestComposeMapper.Map<TestCompose, TestComposeModel>(testCompose);
            return testComposeModelReturn;
        }


        public TestComposeModel PatchTestCompose(int id, JsonPatchDocument<TestCompose> testComposeModelJSON)
        {
            var testComposeEntity = _DAL.PatchTestCompose(id, testComposeModelJSON);

            TestComposeModel testComposeModel = _TestComposeMapper.Map<TestCompose, TestComposeModel>(testComposeEntity);

            return testComposeModel;
        }

        public TestComposeModel PutTestCompose(TestComposeModel testComposeModel)
        {
            TestCompose testComposeEntity = _TestComposeMapper.Map<TestComposeModel, TestCompose>(testComposeModel);
            var testCompose = _DAL.PutTestCompose(testComposeEntity);
            TestComposeModel testComposeModelReturn = _TestComposeMapper.Map<TestCompose, TestComposeModel>(testCompose);
            return testComposeModelReturn;
        }
        public void DeleteTestCompose(int id)
        {
            _DAL.DeleteTestCompose(id);
        }
    }
}


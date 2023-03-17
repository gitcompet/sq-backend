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
    public class TestBLL : InterfaceTest
    {

        private TestDAL _DAL;
        private Mapper _TestMapper;

        public TestBLL()
        {
            _DAL = new Data_Access_Layer.DAL.TestDAL();
            var _configTest = new MapperConfiguration(cfg => cfg.CreateMap<Test, TestModel>().ReverseMap());

            _TestMapper = new Mapper(_configTest);
        }

        public List<TestModel> GetAllTest()
        {
            List<Test> testFromDB = _DAL.GetAllTest();
            List<TestModel> testModel = _TestMapper.Map<List<Test>, List<TestModel>>(testFromDB);

            return testModel;
        }

        public TestModel GetTestById(int id)
        {
            var testEntity = _DAL.GetTestById(id);

            TestModel testModel = _TestMapper.Map<Test, TestModel>(testEntity);

            return testModel;
        }


        public TestModel PostTest(TestModel testModel)
        {
            Test testEntity = _TestMapper.Map<TestModel, Test>(testModel);
            var test = _DAL.PostTest(testEntity);
            TestModel testModelReturn = _TestMapper.Map<Test, TestModel>(test);
            return testModelReturn;
        }


        public TestModel PatchTest(int id, JsonPatchDocument<Test> testModelJSON)
        {
            var testEntity = _DAL.PatchTest(id, testModelJSON);

            TestModel testModel = _TestMapper.Map<Test, TestModel>(testEntity);

            return testModel;
        }

        public TestModel PutTest(TestModel testModel)
        {
            Test testEntity = _TestMapper.Map<TestModel, Test>(testModel);
            var test = _DAL.PutTest(testEntity);
            TestModel testModelReturn = _TestMapper.Map<Test, TestModel>(test);
            return testModelReturn;
        }
        public void DeleteTest(int id)
        {
            _DAL.DeleteTest(id);
        }
    }
}


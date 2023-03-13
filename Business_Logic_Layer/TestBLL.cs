using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;

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


        public void PostTest(TestModel testModel)
        {
            Test testEntity = _TestMapper.Map<TestModel, Test>(testModel);
            _DAL.postTest(testEntity);
        }

    }
}
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
    public class TestCategoryComposeBLL : InterfaceTestCategoryCompose
    {

        private TestCategoryComposeDAL _DAL;
        private Mapper _TestCategoryComposeMapper;

        public TestCategoryComposeBLL()
        {
            _DAL = new Data_Access_Layer.DAL.TestCategoryComposeDAL();
            var _configTestCategoryCompose = new MapperConfiguration(cfg => cfg.CreateMap<TestCategoryCompose, TestCategoryComposeModel>().ReverseMap());

            _TestCategoryComposeMapper = new Mapper(_configTestCategoryCompose);
        }

        public List<TestCategoryComposeModel> GetAllTestCategoryCompose()
        {
            List<TestCategoryCompose> testCategoryComposeFromDB = _DAL.GetAllTestCategoryCompose();
            List<TestCategoryComposeModel> testCategoryComposeModel = _TestCategoryComposeMapper.Map<List<TestCategoryCompose>, List<TestCategoryComposeModel>>(testCategoryComposeFromDB);

            return testCategoryComposeModel;
        }

        public TestCategoryComposeModel GetTestCategoryComposeById(int id)
        {
            var testCategoryComposeEntity = _DAL.GetTestCategoryComposeById(id);

            TestCategoryComposeModel testCategoryComposeModel = _TestCategoryComposeMapper.Map<TestCategoryCompose, TestCategoryComposeModel>(testCategoryComposeEntity);

            return testCategoryComposeModel;
        }

        public IEnumerable<TestCategoryComposeModel> GetTestCategoryComposeByTestId(int id)
        {
            var testUserEntity = _DAL.GetTestUserByUserId(id);
            var result = new List<TestCategoryComposeModel>();

            foreach (var item in testUserEntity)
            {
                result.Add(_TestCategoryComposeMapper.Map<TestCategoryCompose, TestCategoryComposeModel>(item));
            }

            return result;
        }

        public TestCategoryComposeModel PostTestCategoryCompose(TestCategoryComposeModel testCategoryComposeModel)
        {
            TestCategoryCompose testCategoryComposeEntity = _TestCategoryComposeMapper.Map<TestCategoryComposeModel, TestCategoryCompose>(testCategoryComposeModel);
            var testCategoryCompose = _DAL.PostTestCategoryCompose(testCategoryComposeEntity);
            TestCategoryComposeModel testCategoryComposeModelReturn = _TestCategoryComposeMapper.Map<TestCategoryCompose, TestCategoryComposeModel>(testCategoryCompose);
            return testCategoryComposeModelReturn;
        }


        public TestCategoryComposeModel PatchTestCategoryCompose(int id, JsonPatchDocument<TestCategoryCompose> testCategoryComposeModelJSON)
        {
            var testCategoryComposeEntity = _DAL.PatchTestCategoryCompose(id, testCategoryComposeModelJSON);

            TestCategoryComposeModel testCategoryComposeModel = _TestCategoryComposeMapper.Map<TestCategoryCompose, TestCategoryComposeModel>(testCategoryComposeEntity);

            return testCategoryComposeModel;
        }

        public TestCategoryComposeModel PutTestCategoryCompose(TestCategoryComposeModel testCategoryComposeModel)
        {
            TestCategoryCompose testCategoryComposeEntity = _TestCategoryComposeMapper.Map<TestCategoryComposeModel, TestCategoryCompose>(testCategoryComposeModel);
            var testCategoryCompose = _DAL.PutTestCategoryCompose(testCategoryComposeEntity);
            TestCategoryComposeModel testCategoryComposeModelReturn = _TestCategoryComposeMapper.Map<TestCategoryCompose, TestCategoryComposeModel>(testCategoryCompose);
            return testCategoryComposeModelReturn;
        }
        public void DeleteTestCategoryCompose(int id)
        {
            _DAL.DeleteTestCategoryCompose(id);
        }
    }
}


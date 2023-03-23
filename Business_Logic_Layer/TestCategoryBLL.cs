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
    public class TestCategoryBLL : InterfaceTestCategory
    {

        private TestCategoryDAL _DAL;
        private Mapper _TestCategoryMapper;

        public TestCategoryBLL()
        {
            _DAL = new Data_Access_Layer.DAL.TestCategoryDAL();
            var _configTestCategory = new MapperConfiguration(cfg => cfg.CreateMap<TestCategory, TestCategoryModel>().ReverseMap());

            _TestCategoryMapper = new Mapper(_configTestCategory);
        }

        public List<TestCategoryModel> GetAllTestCategory()
        {
            List<TestCategory> testCategoryFromDB = _DAL.GetAllTestCategory();
            List<TestCategoryModel> testCategoryModel = _TestCategoryMapper.Map<List<TestCategory>, List<TestCategoryModel>>(testCategoryFromDB);

            return testCategoryModel;
        }

        public TestCategoryModel GetTestCategoryById(int id)
        {
            var testCategoryEntity = _DAL.GetTestCategoryById(id);

            TestCategoryModel testCategoryModel = _TestCategoryMapper.Map<TestCategory, TestCategoryModel>(testCategoryEntity);

            return testCategoryModel;
        }


        public TestCategoryModel PostTestCategory(TestCategoryModel testCategoryModel)
        {
            TestCategory testCategoryEntity = _TestCategoryMapper.Map<TestCategoryModel, TestCategory>(testCategoryModel);
            var testCategory = _DAL.PostTestCategory(testCategoryEntity);
            TestCategoryModel testCategoryModelReturn = _TestCategoryMapper.Map<TestCategory, TestCategoryModel>(testCategory);
            return testCategoryModelReturn;
        }


        public TestCategoryModel PatchTestCategory(int id, JsonPatchDocument<TestCategory> testCategoryModelJSON)
        {
            var testCategoryEntity = _DAL.PatchTestCategory(id, testCategoryModelJSON);

            TestCategoryModel testCategoryModel = _TestCategoryMapper.Map<TestCategory, TestCategoryModel>(testCategoryEntity);

            return testCategoryModel;
        }

        public TestCategoryModel PutTestCategory(TestCategoryModel testCategoryModel)
        {
            TestCategory testCategoryEntity = _TestCategoryMapper.Map<TestCategoryModel, TestCategory>(testCategoryModel);
            var testCategory = _DAL.PutTestCategory(testCategoryEntity);
            TestCategoryModel testCategoryModelReturn = _TestCategoryMapper.Map<TestCategory, TestCategoryModel>(testCategory);
            return testCategoryModelReturn;
        }
        public void DeleteTestCategory(int id)
        {
            _DAL.DeleteTestCategory(id);
        }
    }
}


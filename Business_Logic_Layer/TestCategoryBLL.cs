using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;

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
            List<TestCategory> testcategoryFromDB = _DAL.GetAllTestCategory();
            List<TestCategoryModel> testcategoryModel = _TestCategoryMapper.Map<List<TestCategory>, List<TestCategoryModel>>(testcategoryFromDB);

            return testcategoryModel;
        }

        public TestCategoryModel GetTestCategoryById(int id)
        {
            var testcategoryEntity = _DAL.GetTestCategoryById(id);

            TestCategoryModel testcategoryModel = _TestCategoryMapper.Map<TestCategory, TestCategoryModel>(testcategoryEntity);

            return testcategoryModel;
        }


        public void PostTestCategory(TestCategoryModel testcategoryModel)
        {
            TestCategory testcategoryEntity = _TestCategoryMapper.Map<TestCategoryModel, TestCategory>(testcategoryModel);
            _DAL.postTestCategory(testcategoryEntity);
        }

    }
}


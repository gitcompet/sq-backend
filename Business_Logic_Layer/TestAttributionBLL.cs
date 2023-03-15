using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;

namespace Business_Logic_Layer
{
    public class TestAttributionBLL : InterfaceTestAttribution
    {

        private Data_Access_Layer.TestAttributionDAL _DAL;
        private Mapper _TestAttributionMapper;

        public TestAttributionBLL()
        {
            _DAL = new Data_Access_Layer.TestAttributionDAL();
            var _configTestAttribution = new MapperConfiguration(cfg => cfg.CreateMap<TestAttribution, TestAttributionModel>().ReverseMap());

            _TestAttributionMapper = new Mapper(_configTestAttribution);
        }

        public List<TestAttributionModel> GetAllTestAttribution()
        {
            List<TestAttribution> testattributionFromDB = _DAL.GetAllTestAttribution();
            List<TestAttributionModel> testattributionModel = _TestAttributionMapper.Map<List<TestAttribution>, List<TestAttributionModel>>(testattributionFromDB);

            return testattributionModel;
        }

        public TestAttributionModel GetTestAttributionById(int id)
        {
            var testattributionEntity = _DAL.GetTestAttributionById(id);

            TestAttributionModel testattributionModel = _TestAttributionMapper.Map<TestAttribution, TestAttributionModel>(testattributionEntity);

            return testattributionModel;
        }


        public void PostTestAttribution(TestAttributionModel testattributionModel)
        {
            TestAttribution testattributionEntity = _TestAttributionMapper.Map<TestAttributionModel, TestAttribution>(testattributionModel);
            _DAL.postTestAttribution(testattributionEntity);
        }

    }
}



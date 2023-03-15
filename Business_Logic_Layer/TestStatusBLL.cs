using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;

namespace Business_Logic_Layer
{
    public class TestStatusBLL : InterfaceTestStatus
    {

        private TestStatusDAL _DAL;
        private Mapper _TestStatusMapper;

        public TestStatusBLL()
        {
            _DAL = new Data_Access_Layer.DAL.TestStatusDAL();
            var _configTestStatus = new MapperConfiguration(cfg => cfg.CreateMap<TestStatus, TestStatusModel>().ReverseMap());

            _TestStatusMapper = new Mapper(_configTestStatus);
        }

        public List<TestStatusModel> GetAllTestStatus()
        {
            List<TestStatus> teststatusFromDB = _DAL.GetAllTestStatus();
            List<TestStatusModel> teststatusModel = _TestStatusMapper.Map<List<TestStatus>, List<TestStatusModel>>(teststatusFromDB);

            return teststatusModel;
        }

        public TestStatusModel GetTestStatusById(int id)
        {
            var teststatusEntity = _DAL.GetTestStatusById(id);

            TestStatusModel teststatusModel = _TestStatusMapper.Map<TestStatus, TestStatusModel>(teststatusEntity);

            return teststatusModel;
        }


        public void PostTestStatus(TestStatusModel teststatusModel)
        {
            TestStatus teststatusEntity = _TestStatusMapper.Map<TestStatusModel, TestStatus>(teststatusModel);
            _DAL.postTestStatus(teststatusEntity);
        }

    }
}


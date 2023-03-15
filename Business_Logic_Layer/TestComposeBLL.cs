using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;

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
            List<TestCompose> testcomposeFromDB = _DAL.GetAllTestCompose();
            List<TestComposeModel> testcomposeModel = _TestComposeMapper.Map<List<TestCompose>, List<TestComposeModel>>(testcomposeFromDB);

            return testcomposeModel;
        }

        public TestComposeModel GetTestComposeById(int id)
        {
            var testcomposeEntity = _DAL.GetTestComposeById(id);

            TestComposeModel testcomposeModel = _TestComposeMapper.Map<TestCompose, TestComposeModel>(testcomposeEntity);

            return testcomposeModel;
        }


        public void PostTestCompose(TestComposeModel testcomposeModel)
        {
            TestCompose testcomposeEntity = _TestComposeMapper.Map<TestComposeModel, TestCompose>(testcomposeModel);
            _DAL.postTestCompose(testcomposeEntity);
        }

    }
}


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
    public class TestUserBLL : InterfaceTestUser
    {

        private TestUserDAL _DAL;
        private Mapper _TestUserMapper;

        public TestUserBLL()
        {
            _DAL = new Data_Access_Layer.DAL.TestUserDAL();
            var _configTestUser = new MapperConfiguration(cfg => cfg.CreateMap<TestUser, TestUserModel>().ReverseMap());

            _TestUserMapper = new Mapper(_configTestUser);
        }

        public List<TestUserModel> GetAllTestUser()
        {
            List<TestUser> testUserFromDB = _DAL.GetAllTestUser();
            List<TestUserModel> testUserModel = _TestUserMapper.Map<List<TestUser>, List<TestUserModel>>(testUserFromDB);

            return testUserModel;
        }

        public TestUserModel GetTestUserById(int id)
        {
            var testUserEntity = _DAL.GetTestUserById(id);

            TestUserModel testUserModel = _TestUserMapper.Map<TestUser, TestUserModel>(testUserEntity);

            return testUserModel;
        }


        public TestUserModel PostTestUser(TestUserModel testUserModel)
        {
            TestUser testUserEntity = _TestUserMapper.Map<TestUserModel, TestUser>(testUserModel);
            var testUser = _DAL.PostTestUser(testUserEntity);
            TestUserModel testUserModelReturn = _TestUserMapper.Map<TestUser, TestUserModel>(testUser);
            return testUserModelReturn;
        }


        public TestUserModel PatchTestUser(int id, JsonPatchDocument<TestUser> testUserModelJSON)
        {
            var testUserEntity = _DAL.PatchTestUser(id, testUserModelJSON);

            TestUserModel testUserModel = _TestUserMapper.Map<TestUser, TestUserModel>(testUserEntity);

            return testUserModel;
        }

        public TestUserModel PutTestUser(TestUserModel testUserModel)
        {
            TestUser testUserEntity = _TestUserMapper.Map<TestUserModel, TestUser>(testUserModel);
            var testUser = _DAL.PutTestUser(testUserEntity);
            TestUserModel testUserModelReturn = _TestUserMapper.Map<TestUser, TestUserModel>(testUser);
            return testUserModelReturn;
        }
        public void DeleteTestUser(int id)
        {
            _DAL.DeleteTestUser(id);
        }
    }
}


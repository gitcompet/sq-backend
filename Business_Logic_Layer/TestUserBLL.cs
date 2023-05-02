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
        private QuizUserDAL _QuizUserDAL;
        private Mapper _TestUserMapper;

        public TestUserBLL()
        {
            _DAL = new Data_Access_Layer.DAL.TestUserDAL();
            _QuizUserDAL = new Data_Access_Layer.DAL.QuizUserDAL();
            var _configTestUser = new MapperConfiguration(cfg => cfg.CreateMap<TestUser, TestUserModel>().ReverseMap());

            _TestUserMapper = new Mapper(_configTestUser);
        }

        public List<TestUserModel> GetAllTestUser()
        {
            List<TestUser> testUserFromDB = _DAL.GetAllTestUser();
            foreach (var testUser in testUserFromDB)
            {

                if (testUser == null)
                {
                    return null;
                }
                var Quizes = _QuizUserDAL.GetQuizUserByLinkId(testUser.TestUserId);
                bool isClosed = true;
                foreach (var quiz in Quizes)
                {
                    if (quiz.IsClosed == false)
                    {
                        isClosed = false;
                        break;
                    }
                }
                if (isClosed)
                {
                    testUser.TestStatus = 1;
                    _DAL.PutTestUser(testUser);
                }
            }
            List<TestUserModel> testUserModel = _TestUserMapper.Map<List<TestUser>, List<TestUserModel>>(testUserFromDB);

            return testUserModel;
        }

        public TestUserModel GetTestUserById(int id, bool isPending)
        {
            var testUserEntity = _DAL.GetTestUserById(id, isPending);
            if (testUserEntity == null)
            {
                return null;
            }
            var Quizes = _QuizUserDAL.GetQuizUserByLinkId(testUserEntity.TestUserId);
            bool isClosed = true;
            foreach (var quiz in Quizes)
            {
                if (quiz.IsClosed == false)
                {
                    isClosed = false;
                    break;
                }
            }
            if (isClosed)
            {
                testUserEntity.TestStatus = 1;
                _DAL.PutTestUser(testUserEntity);
            }
            TestUserModel testUserModel = _TestUserMapper.Map<TestUser, TestUserModel>(testUserEntity);

            return testUserModel;
        }

        public IEnumerable<TestUserModel> GetTestUserByUserId(int id, bool isPending)
        {
            var testUserEntity = _DAL.GetTestUserByUserId(id, isPending);
            var result = new List<TestUserModel>();
            if (testUserEntity == null)
            {
                return null;
            }
            foreach (var test in testUserEntity)
            {
                var Quizes = _QuizUserDAL.GetQuizUserByLinkId(test.TestUserId);
                bool isClosed = true;
                foreach (var quiz in Quizes)
                {
                    if (quiz.IsClosed == false)
                    {
                        isClosed = false;
                        break;
                    }
                }
                if (isClosed)
                {
                    test.TestStatus = 1;
                    _DAL.PutTestUser(test);
                }

            }
            foreach (var item in testUserEntity)
            {
                result.Add(_TestUserMapper.Map<TestUser, TestUserModel>(item));
            }

            return result;
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


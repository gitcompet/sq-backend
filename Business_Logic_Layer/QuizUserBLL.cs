using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class QuizUserBLL : InterfaceQuizUser
    {

        private QuizUserDAL _DAL;
        private QuestionUserDAL _DALQuestionUser;
        private QuestionDAL _DALQuestion;
        private AnswerUserDAL _DALAnswerUser;
        private AnswerQuestionDAL _DALAnswerQuestion;
        private AnonQuizScoreDAL _DALAnonQuizScore;
        private QuizComposeBLL _BLLQuizCompose;
        private Mapper _QuizUserMapper;

        public QuizUserBLL()
        {
            _DAL = new Data_Access_Layer.DAL.QuizUserDAL();
            var _configQuizUser = new MapperConfiguration(cfg => cfg.CreateMap<QuizUser, QuizUserModel>().ReverseMap());
            _BLLQuizCompose = new QuizComposeBLL();
            _DALAnswerQuestion = new AnswerQuestionDAL();
            _DALQuestionUser = new QuestionUserDAL();
            _DALQuestion = new QuestionDAL();
            _DALAnonQuizScore = new AnonQuizScoreDAL();
            _DALAnswerUser = new AnswerUserDAL();
            _QuizUserMapper = new Mapper(_configQuizUser);
        }

        private QuizUserModel GetScoredByQuizUser(QuizUserModel quizUserModel)
        {

            if (quizUserModel.IsClosed)
            {
                float totalWeight = 0;
                List<int> answers = new List<int>();
                List<int> expectedAnswer = new List<int>();
                float score = 0;
                //for all question, get the answer made and the actual answers
                var questionList = _BLLQuizCompose.GetQuizComposeByQuizId(int.Parse(quizUserModel.QuizId));
                List<QuestionUser> questionUserIds = _DALQuestionUser.GetQuestionUserByLinkId(int.Parse(quizUserModel.QuizUserId), true).ToList();
                int questionUserId;
                foreach (var question in questionList)
                {
                    expectedAnswer = _DALAnswerQuestion.GetGoodAnswerList(int.Parse(question.QuestionId)); //validé
                    questionUserId = questionUserIds.FirstOrDefault(x => x.QuestionId == int.Parse(question.QuestionId)).QuestionUserId;
                    answers = _DALAnswerUser.GetAnswerUserByLinkId(questionUserId).Select(x => x.AnswerId).ToList();
                    float weight = _DALQuestion.GetWeight(int.Parse(question.QuestionId));
                    totalWeight += weight;
                    if (answers.SequenceEqual(expectedAnswer))
                    {
                        score = score + weight;
                    }
                }
                quizUserModel.Score = score/totalWeight*100;
                //now we evaluate comparing to other ones
                quizUserModel.ranking = _DALAnonQuizScore.GetEvaluation(score, int.Parse(quizUserModel.QuizId));
            }
            return quizUserModel;
        }

        public List<QuizUserModel> GetAllQuizUser()
        {
            List<QuizUser> quizUserFromDB = _DAL.GetAllQuizUser();
            List<QuizUserModel> quizUserModel = _QuizUserMapper.Map<List<QuizUser>, List<QuizUserModel>>(quizUserFromDB);

            return quizUserModel;
        }

        public QuizUserModel GetQuizUserById(int id)
        {
            var quizUserEntity = _DAL.GetQuizUserById(id);

            QuizUserModel quizUserModel = _QuizUserMapper.Map<QuizUser, QuizUserModel>(quizUserEntity);
            quizUserModel = GetScoredByQuizUser(quizUserModel);

            return quizUserModel;
        }

        public ActionResult<IEnumerable<QuizUserModel>> GetQuizUserByLinkId(int id)
        {
            var quizUserEntity = _DAL.GetQuizUserByLinkId(id);
            var result = new List<QuizUserModel>();

            foreach (var item in quizUserEntity)
            {
                QuizUserModel model = _QuizUserMapper.Map<QuizUser, QuizUserModel>(item);
                result.Add(GetScoredByQuizUser(model));
            }

            return result;
        }


        public QuizUserModel PostQuizUser(QuizUserModel quizUserModel)
        {
            QuizUser quizUserEntity = _QuizUserMapper.Map<QuizUserModel, QuizUser>(quizUserModel);
            var quizUser = _DAL.PostQuizUser(quizUserEntity);
            QuizUserModel quizUserModelReturn = _QuizUserMapper.Map<QuizUser, QuizUserModel>(quizUser);
            return quizUserModelReturn;
        }


        public QuizUserModel PatchQuizUser(int id, JsonPatchDocument<QuizUser> quizUserModelJSON)
        {
            var quizUserEntity = _DAL.PatchQuizUser(id, quizUserModelJSON);

            QuizUserModel quizUserModel = _QuizUserMapper.Map<QuizUser, QuizUserModel>(quizUserEntity);

            return quizUserModel;
        }

        public QuizUserModel PutQuizUser(QuizUserModel quizUserModel)
        {
            QuizUser quizUserEntity = _QuizUserMapper.Map<QuizUserModel, QuizUser>(quizUserModel);
            var quizUser = _DAL.PutQuizUser(quizUserEntity);
            QuizUserModel quizUserModelReturn = _QuizUserMapper.Map<QuizUser, QuizUserModel>(quizUser);
            return quizUserModelReturn;
        }
        public void DeleteQuizUser(int id)
        {
            _DAL.DeleteQuizUser(id);
        }
        public List<QuizUserModel> GetLastQuizValidates(int qty)
        {
            var temp = _DAL.GetLastQuizValidates(qty);
            List<QuizUserModel> result = _QuizUserMapper.Map<List<QuizUser>, List<QuizUserModel>>(temp);

            return result;
        }
        public List<QuizUserModel> GetPendingQuizes(int qty)
        {
            var temp = _DAL.GetPendingQuizes(qty);
            List<QuizUserModel> result = _QuizUserMapper.Map<List<QuizUser>, List<QuizUserModel>>(temp);

            return result;
        }
    }
}


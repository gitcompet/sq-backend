using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;
using Microsoft.AspNetCore.JsonPatch;
using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class AnswerBLL : InterfaceAnswer
    {

        private AnswerDAL _DAL;
        private ElementTranslationDAL _DALLibelle;
        private Mapper _AnswerMapper;

        public AnswerBLL()
        {
            _DAL = new AnswerDAL();
            _DALLibelle = new ElementTranslationDAL();
            var _configAnswer = new MapperConfiguration(cfg => cfg.CreateMap<Answer, AnswerModel>().ReverseMap());

            _AnswerMapper = new Mapper(_configAnswer);
        }

        public List<AnswerModel> GetAllAnswer()
        {
            List<Answer> answerFromDB = _DAL.GetAllAnswer();
            List<AnswerModel> answerModel = _AnswerMapper.Map<List<Answer>, List<AnswerModel>>(answerFromDB);

            return answerModel;
        }

        public AnswerModel GetAnswerById(int id)
        {
            var answerEntity = _DAL.GetAnswerById(id);

            AnswerModel answerModel = _AnswerMapper.Map<Answer, AnswerModel>(answerEntity);

            return answerModel;
        }

        public IEnumerable<String> GetAnswerByListId(IEnumerable<String> ids, string elementType, int languageId)
        {
            var answerList = _DALLibelle.GetAnswerByListId(ids, elementType, languageId);



            return answerList;
        }


        public void PostAnswer(int id)
        {
            /*var answerModel = new AnswerModel(id);
            Answer answerEntity = _AnswerMapper.Map<AnswerModel, Answer>(answerModel);*/
        }


        public AnswerModel PatchAnswer(int id, JsonPatchDocument<Answer> answerModelJSON)
        {
            var answerEntity = _DAL.PatchAnswer(id, answerModelJSON);

            AnswerModel answerModel = _AnswerMapper.Map<Answer, AnswerModel>(answerEntity);

            return answerModel;
        }

        public AnswerModel PutAnswer(AnswerModel answerModel)
        {
            Answer answerEntity = _AnswerMapper.Map<AnswerModel, Answer>(answerModel);
            var answer = _DAL.PutAnswer(answerEntity);
            AnswerModel answerModelReturn = _AnswerMapper.Map<Answer, AnswerModel>(answer);
            return answerModelReturn;
        }
        public void DeleteAnswer(int id)
        {
            _DAL.DeleteAnswer(id);
        }
    }
}


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
    public class LanguageBLL : InterfaceLanguage
    {

        private LanguageDAL _DAL;
        private Mapper _LanguageMapper;

        public LanguageBLL()
        {
            _DAL = new Data_Access_Layer.DAL.LanguageDAL();
            var _configLanguage = new MapperConfiguration(cfg => cfg.CreateMap<Language, LanguageModel>().ReverseMap());

            _LanguageMapper = new Mapper(_configLanguage);
        }

        public List<LanguageModel> GetAllLanguage()
        {
            List<Language> languageFromDB = _DAL.GetAllLanguage();
            List<LanguageModel> languageModel = _LanguageMapper.Map<List<Language>, List<LanguageModel>>(languageFromDB);

            return languageModel;
        }

        public LanguageModel GetLanguageById(int id)
        {
            var languageEntity = _DAL.GetLanguageById(id);

            LanguageModel languageModel = _LanguageMapper.Map<Language, LanguageModel>(languageEntity);

            return languageModel;
        }


        public LanguageModel PostLanguage(LanguageModel languageModel)
        {
            Language languageEntity = _LanguageMapper.Map<LanguageModel, Language>(languageModel);
            var language = _DAL.PostLanguage(languageEntity);
            LanguageModel languageModelReturn = _LanguageMapper.Map<Language, LanguageModel>(language);
            return languageModelReturn;
        }


        public LanguageModel PatchLanguage(int id, JsonPatchDocument<Language> languageModelJSON)
        {
            var languageEntity = _DAL.PatchLanguage(id, languageModelJSON);

            LanguageModel languageModel = _LanguageMapper.Map<Language, LanguageModel>(languageEntity);

            return languageModel;
        }

        public LanguageModel PutLanguage(LanguageModel languageModel)
        {
            Language languageEntity = _LanguageMapper.Map<LanguageModel, Language>(languageModel);
            var language = _DAL.PutLanguage(languageEntity);
            LanguageModel languageModelReturn = _LanguageMapper.Map<Language, LanguageModel>(language);
            return languageModelReturn;
        }
        public void DeleteLanguage(int id)
        {
            _DAL.DeleteLanguage(id);
        }
    }
}


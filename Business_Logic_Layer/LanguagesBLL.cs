using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;

namespace Business_Logic_Layer
{
    public class LanguagesBLL : InterfaceLanguages 
        {

        private LanguagesDAL _DAL;
        private Mapper _LanguagesMapper;

        public LanguagesBLL()
        {
            _DAL = new Data_Access_Layer.DAL.LanguagesDAL();
            var _configLanguages = new MapperConfiguration(cfg => cfg.CreateMap<Languages, LanguagesModel>().ReverseMap());

            _LanguagesMapper = new Mapper(_configLanguages);
        }

        public List<LanguagesModel> GetAllLanguages()
        {
            List<Languages> languagesFromDB = _DAL.GetAllLanguages();
            List<LanguagesModel> personsModel = _LanguagesMapper.Map<List<Languages>, List<LanguagesModel>>(languagesFromDB);

            return personsModel;
        }
        public LanguagesModel GetLanguagesById(int id)
        {
            var languagesEntity = _DAL.GetLanguagesById(id);

            LanguagesModel languagesModel = _LanguagesMapper.Map<Languages, LanguagesModel>(languagesEntity);

            return languagesModel;
        }

        public int RemoveLanguage(int id)
        {
            return _DAL.RemoveLanguage(id);
        }

        public int PostLanguages(LanguagesModelDTO languagesModelDTO)
        {
            var languagesModel = new LanguagesModel(languagesModelDTO);
            Languages languagesEntity = _LanguagesMapper.Map<LanguagesModel, Languages>(languagesModel);
            return _DAL.PostLanguages(languagesEntity);
        }

    }
}


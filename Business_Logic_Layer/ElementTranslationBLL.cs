using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;

namespace Business_Logic_Layer
{
    public class ElementTranslationBLL : InterfaceElementTranslation
    {

        private Data_Access_Layer.ElementTranslationDAL _DAL;
        private Mapper _ElementTranslationMapper;

        public ElementTranslationBLL()
        {
            _DAL = new Data_Access_Layer.ElementTranslationDAL();
            var _configElementTranslation = new MapperConfiguration(cfg => cfg.CreateMap<ElementTranslation, ElementTranslationModel>().ReverseMap());

            _ElementTranslationMapper = new Mapper(_configElementTranslation);
        }

        public List<ElementTranslationModel> GetAllElementTranslation()
        {
            List<ElementTranslation> elementtranslationFromDB = _DAL.GetAllElementTranslation();
            List<ElementTranslationModel> elementtranslationModel = _ElementTranslationMapper.Map<List<ElementTranslation>, List<ElementTranslationModel>>(elementtranslationFromDB);

            return elementtranslationModel;
        }

        public ElementTranslationModel GetElementTranslationById(int id)
        {
            var elementtranslationEntity = _DAL.GetElementTranslationById(id);

            ElementTranslationModel elementtranslationModel = _ElementTranslationMapper.Map<ElementTranslation, ElementTranslationModel>(elementtranslationEntity);

            return elementtranslationModel;
        }


        public void PostElementTranslation(ElementTranslationModel elementtranslationModel)
        {
            ElementTranslation elementtranslationEntity = _ElementTranslationMapper.Map<ElementTranslationModel, ElementTranslation>(elementtranslationModel);
            _DAL.postElementTranslation(elementtranslationEntity);
        }

    }
}
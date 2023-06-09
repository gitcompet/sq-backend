﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;
using Microsoft.AspNetCore.JsonPatch;

namespace Business_Logic_Layer
{
    public class ElementTranslationBLL : InterfaceElementTranslation
    {

        private ElementTranslationDAL _DAL;
        private Mapper _ElementTranslationMapper;

        public ElementTranslationBLL()
        {
            _DAL = new Data_Access_Layer.DAL.ElementTranslationDAL();
            var _configElementTranslation = new MapperConfiguration(cfg => cfg.CreateMap<ElementTranslation, ElementTranslationModel>().ReverseMap());

            _ElementTranslationMapper = new Mapper(_configElementTranslation);
        }

        public List<ElementTranslationModel> GetAllElementTranslation()
        {
            List<ElementTranslation> elementTranslationFromDB = _DAL.GetAllElementTranslation();
            List<ElementTranslationModel> elementTranslationModel = _ElementTranslationMapper.Map<List<ElementTranslation>, List<ElementTranslationModel>>(elementTranslationFromDB);

            return elementTranslationModel;
        }

        public ElementTranslationModel GetElementTranslationById(int id)
        {
            var elementTranslationEntity = _DAL.GetElementTranslationById(id);

            ElementTranslationModel elementTranslationModel = _ElementTranslationMapper.Map<ElementTranslation, ElementTranslationModel>(elementTranslationEntity);

            return elementTranslationModel;
        }
        public ElementTranslationModel GetElementTranslationByKey(int id, string elementType, int languageId)
        {
            var elementTranslationEntity = _DAL.GetElementTranslationByKey(id, elementType, languageId);

            ElementTranslationModel elementTranslationModel = _ElementTranslationMapper.Map<ElementTranslation, ElementTranslationModel>(elementTranslationEntity);

            return elementTranslationModel;
        }

        public string GetElementLabelById(string id, string elementType, int languageId)
        {
            return _DAL.GetElementLabelById(id, elementType, languageId);
        }
        public IEnumerable<string> GetElementsLabelById(IEnumerable<string> id, string elementType, int languageId)
        {
            return _DAL.GetElementsLabelById(id, elementType, languageId);
        }


        public ElementTranslationModel PostElementTranslation(ElementTranslationModel elementTranslationModel)
        {
            ElementTranslation elementTranslationEntity = _ElementTranslationMapper.Map<ElementTranslationModel, ElementTranslation>(elementTranslationModel);
            var elementTranslation = _DAL.PostElementTranslation(elementTranslationEntity);
            ElementTranslationModel elementTranslationModelReturn = _ElementTranslationMapper.Map<ElementTranslation, ElementTranslationModel>(elementTranslation);
            return elementTranslationModelReturn;
        }


        public ElementTranslationModel PatchElementTranslation(int id, JsonPatchDocument<ElementTranslation> elementTranslationModelJSON)
        {
            var elementTranslationEntity = _DAL.PatchElementTranslation(id, elementTranslationModelJSON);

            ElementTranslationModel elementTranslationModel = _ElementTranslationMapper.Map<ElementTranslation, ElementTranslationModel>(elementTranslationEntity);

            return elementTranslationModel;
        }

        public ElementTranslationModel PutElementTranslation(ElementTranslationModel elementTranslationModel)
        {
            ElementTranslation elementTranslationEntity = _ElementTranslationMapper.Map<ElementTranslationModel, ElementTranslation>(elementTranslationModel);
            var elementTranslation = _DAL.PutElementTranslation(elementTranslationEntity);
            ElementTranslationModel elementTranslationModelReturn = _ElementTranslationMapper.Map<ElementTranslation, ElementTranslationModel>(elementTranslation);
            return elementTranslationModelReturn;
        }
        public void DeleteElementTranslation(int id)
        {
            _DAL.DeleteElementTranslation(id);
        }
        public void DeleteElementTranslationByItem(int id, string type)
        {
            _DAL.DeleteElementTranslationByItem(id, type);
        }
    }
}


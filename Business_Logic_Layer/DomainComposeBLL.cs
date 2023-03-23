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
    public class DomainComposeBLL : InterfaceDomainCompose
    {

        private DomainComposeDAL _DAL;
        private Mapper _DomainComposeMapper;

        public DomainComposeBLL()
        {
            _DAL = new Data_Access_Layer.DAL.DomainComposeDAL();
            var _configDomainCompose = new MapperConfiguration(cfg => cfg.CreateMap<DomainCompose, DomainComposeModel>().ReverseMap());

            _DomainComposeMapper = new Mapper(_configDomainCompose);
        }

        public List<DomainComposeModel> GetAllDomainCompose()
        {
            List<DomainCompose> domainComposeFromDB = _DAL.GetAllDomainCompose();
            List<DomainComposeModel> domainComposeModel = _DomainComposeMapper.Map<List<DomainCompose>, List<DomainComposeModel>>(domainComposeFromDB);

            return domainComposeModel;
        }

        public DomainComposeModel GetDomainComposeById(int id)
        {
            var domainComposeEntity = _DAL.GetDomainComposeById(id);

            DomainComposeModel domainComposeModel = _DomainComposeMapper.Map<DomainCompose, DomainComposeModel>(domainComposeEntity);

            return domainComposeModel;
        }


        public DomainComposeModel PostDomainCompose(DomainComposeModel domainComposeModel)
        {
            DomainCompose domainComposeEntity = _DomainComposeMapper.Map<DomainComposeModel, DomainCompose>(domainComposeModel);
            var domainCompose = _DAL.PostDomainCompose(domainComposeEntity);
            DomainComposeModel domainComposeModelReturn = _DomainComposeMapper.Map<DomainCompose, DomainComposeModel>(domainCompose);
            return domainComposeModelReturn;
        }


        public DomainComposeModel PatchDomainCompose(int id, JsonPatchDocument<DomainCompose> domainComposeModelJSON)
        {
            var domainComposeEntity = _DAL.PatchDomainCompose(id, domainComposeModelJSON);

            DomainComposeModel domainComposeModel = _DomainComposeMapper.Map<DomainCompose, DomainComposeModel>(domainComposeEntity);

            return domainComposeModel;
        }

        public DomainComposeModel PutDomainCompose(DomainComposeModel domainComposeModel)
        {
            DomainCompose domainComposeEntity = _DomainComposeMapper.Map<DomainComposeModel, DomainCompose>(domainComposeModel);
            var domainCompose = _DAL.PutDomainCompose(domainComposeEntity);
            DomainComposeModel domainComposeModelReturn = _DomainComposeMapper.Map<DomainCompose, DomainComposeModel>(domainCompose);
            return domainComposeModelReturn;
        }
        public void DeleteDomainCompose(int id)
        {
            _DAL.DeleteDomainCompose(id);
        }
    }
}


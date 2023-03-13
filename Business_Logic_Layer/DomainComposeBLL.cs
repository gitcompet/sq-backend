using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;

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
            List<DomainCompose> domaincomposeFromDB = _DAL.GetAllDomainCompose();
            List<DomainComposeModel> domaincomposeModel = _DomainComposeMapper.Map<List<DomainCompose>, List<DomainComposeModel>>(domaincomposeFromDB);

            return domaincomposeModel;
        }

        public DomainComposeModel GetDomainComposeById(int id)
        {
            var domaincomposeEntity = _DAL.GetDomainComposeById(id);

            DomainComposeModel domaincomposeModel = _DomainComposeMapper.Map<DomainCompose, DomainComposeModel>(domaincomposeEntity);

            return domaincomposeModel;
        }


        public void PostDomainCompose(DomainComposeModel domaincomposeModel)
        {
            DomainCompose domaincomposeEntity = _DomainComposeMapper.Map<DomainComposeModel, DomainCompose>(domaincomposeModel);
            _DAL.postDomainCompose(domaincomposeEntity);
        }

    }
}


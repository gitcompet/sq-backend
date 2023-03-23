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
    public class DomainBLL : InterfaceDomain
    {

        private DomainDAL _DAL;
        private Mapper _DomainMapper;

        public DomainBLL()
        {
            _DAL = new Data_Access_Layer.DAL.DomainDAL();
            var _configDomain = new MapperConfiguration(cfg => cfg.CreateMap<Domain, DomainModel>().ReverseMap());

            _DomainMapper = new Mapper(_configDomain);
        }

        public List<DomainModel> GetAllDomain()
        {
            List<Domain> domainFromDB = _DAL.GetAllDomain();
            List<DomainModel> domainModel = _DomainMapper.Map<List<Domain>, List<DomainModel>>(domainFromDB);

            return domainModel;
        }

        public DomainModel GetDomainById(int id)
        {
            var domainEntity = _DAL.GetDomainById(id);

            DomainModel domainModel = _DomainMapper.Map<Domain, DomainModel>(domainEntity);

            return domainModel;
        }


        public DomainModel PostDomain(DomainModel domainModel)
        {
            Domain domainEntity = _DomainMapper.Map<DomainModel, Domain>(domainModel);
            var domain = _DAL.PostDomain(domainEntity);
            DomainModel domainModelReturn = _DomainMapper.Map<Domain, DomainModel>(domain);
            return domainModelReturn;
        }


        public DomainModel PatchDomain(int id, JsonPatchDocument<Domain> domainModelJSON)
        {
            var domainEntity = _DAL.PatchDomain(id, domainModelJSON);

            DomainModel domainModel = _DomainMapper.Map<Domain, DomainModel>(domainEntity);

            return domainModel;
        }

        public DomainModel PutDomain(DomainModel domainModel)
        {
            Domain domainEntity = _DomainMapper.Map<DomainModel, Domain>(domainModel);
            var domain = _DAL.PutDomain(domainEntity);
            DomainModel domainModelReturn = _DomainMapper.Map<Domain, DomainModel>(domain);
            return domainModelReturn;
        }
        public void DeleteDomain(int id)
        {
            _DAL.DeleteDomain(id);
        }
    }
}


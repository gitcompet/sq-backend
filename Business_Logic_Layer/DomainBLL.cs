﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;

namespace Business_Logic_Layer
{
    public class DomainBLL : InterfaceDomain 
        {

        private Data_Access_Layer.DomainDAL _DAL;
        private Mapper _DomainMapper;

        public DomainBLL()
        {
            _DAL = new Data_Access_Layer.DomainDAL();
            var _configDomain = new MapperConfiguration(cfg => cfg.CreateMap<Domain, DomainModel>().ReverseMap());

            _DomainMapper = new Mapper(_configDomain);
        }

        public List<DomainModel> GetAllDomain()
        {
            List<Domain> domainFromDB = _DAL.GetAllDomain();
            List<DomainModel> personsModel = _DomainMapper.Map<List<Domain>, List<DomainModel>>(domainFromDB);

            return personsModel;
        }

        public DomainModel GetDomainById(int id)
        {
            var domainEntity = _DAL.GetDomainById(id);

            DomainModel domainModel = _DomainMapper.Map<Domain, DomainModel>(domainEntity);

            return domainModel;
        }


        public void PostDomain(DomainModel domainModel)
        {
            Domain domainEntity = _DomainMapper.Map<DomainModel, Domain>(domainModel);
            _DAL.postDomain(domainEntity);
        }

    }
}


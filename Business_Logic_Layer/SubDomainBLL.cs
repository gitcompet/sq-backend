using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;

namespace Business_Logic_Layer
{
    public class SubDomainBLL : InterfaceSubDomain
    {

        private SubDomainDAL _DAL;
        private Mapper _SubDomainMapper;

        public SubDomainBLL()
        {
            _DAL = new Data_Access_Layer.DAL.SubDomainDAL();
            var _configSubDomain = new MapperConfiguration(cfg => cfg.CreateMap<SubDomain, SubDomainModel>().ReverseMap());

            _SubDomainMapper = new Mapper(_configSubDomain);
        }

        public List<SubDomainModel> GetAllSubDomain()
        {
            List<SubDomain> subdomainFromDB = _DAL.GetAllSubDomain();
            List<SubDomainModel> subdomainModel = _SubDomainMapper.Map<List<SubDomain>, List<SubDomainModel>>(subdomainFromDB);

            return subdomainModel;
        }

        public SubDomainModel GetSubDomainById(int id)
        {
            var subdomainEntity = _DAL.GetSubDomainById(id);

            SubDomainModel subdomainModel = _SubDomainMapper.Map<SubDomain, SubDomainModel>(subdomainEntity);

            return subdomainModel;
        }


        public void PostSubDomain(SubDomainModel subdomainModel)
        {
            SubDomain subdomainEntity = _SubDomainMapper.Map<SubDomainModel, SubDomain>(subdomainModel);
            _DAL.postSubDomain(subdomainEntity);
        }

    }
}


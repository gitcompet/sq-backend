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
            List<SubDomain> subDomainFromDB = _DAL.GetAllSubDomain();
            List<SubDomainModel> subDomainModel = _SubDomainMapper.Map<List<SubDomain>, List<SubDomainModel>>(subDomainFromDB);

            return subDomainModel;
        }

        public SubDomainModel GetSubDomainById(int id)
        {
            var subDomainEntity = _DAL.GetSubDomainById(id);

            SubDomainModel subDomainModel = _SubDomainMapper.Map<SubDomain, SubDomainModel>(subDomainEntity);

            return subDomainModel;
        }


        public SubDomainModel PostSubDomain(SubDomainModel subDomainModel)
        {
            SubDomain subDomainEntity = _SubDomainMapper.Map<SubDomainModel, SubDomain>(subDomainModel);
            var subDomain = _DAL.PostSubDomain(subDomainEntity);
            SubDomainModel subDomainModelReturn = _SubDomainMapper.Map<SubDomain, SubDomainModel>(subDomain);
            return subDomainModelReturn;
        }


        public SubDomainModel PatchSubDomain(int id, JsonPatchDocument<SubDomain> subDomainModelJSON)
        {
            var subDomainEntity = _DAL.PatchSubDomain(id, subDomainModelJSON);

            SubDomainModel subDomainModel = _SubDomainMapper.Map<SubDomain, SubDomainModel>(subDomainEntity);

            return subDomainModel;
        }

        public SubDomainModel PutSubDomain(SubDomainModel subDomainModel)
        {
            SubDomain subDomainEntity = _SubDomainMapper.Map<SubDomainModel, SubDomain>(subDomainModel);
            var subDomain = _DAL.PutSubDomain(subDomainEntity);
            SubDomainModel subDomainModelReturn = _SubDomainMapper.Map<SubDomain, SubDomainModel>(subDomain);
            return subDomainModelReturn;
        }
        public void DeleteSubDomain(int id)
        {
            _DAL.DeleteSubDomain(id);
        }
    }
}


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
    public class SubDomainComposeBLL : InterfaceSubDomainCompose
    {

        private SubDomainComposeDAL _DAL;
        private Mapper _SubDomainComposeMapper;

        public SubDomainComposeBLL()
        {
            _DAL = new Data_Access_Layer.DAL.SubDomainComposeDAL();
            var _configSubDomainCompose = new MapperConfiguration(cfg => cfg.CreateMap<SubDomainCompose, SubDomainComposeModel>().ReverseMap());

            _SubDomainComposeMapper = new Mapper(_configSubDomainCompose);
        }

        public List<SubDomainComposeModel> GetAllSubDomainCompose()
        {
            List<SubDomainCompose> subDomainComposeFromDB = _DAL.GetAllSubDomainCompose();
            List<SubDomainComposeModel> subDomainComposeModel = _SubDomainComposeMapper.Map<List<SubDomainCompose>, List<SubDomainComposeModel>>(subDomainComposeFromDB);

            return subDomainComposeModel;
        }

        public SubDomainComposeModel GetSubDomainComposeById(int id)
        {
            var subDomainComposeEntity = _DAL.GetSubDomainComposeById(id);

            SubDomainComposeModel subDomainComposeModel = _SubDomainComposeMapper.Map<SubDomainCompose, SubDomainComposeModel>(subDomainComposeEntity);

            return subDomainComposeModel;
        }

        public IEnumerable<SubDomainComposeModel> GetSubDomainComposeByElementId(string type, int id)
        {
            var testUserEntity = _DAL.GetSubDomainComposeByElementId(type, id);
            var result = new List<SubDomainComposeModel>();

            foreach (var item in testUserEntity)
            {
                result.Add(_SubDomainComposeMapper.Map<SubDomainCompose, SubDomainComposeModel>(item));
            }

            return result;
        }

        public SubDomainComposeModel PostSubDomainCompose(SubDomainComposeModel subDomainComposeModel)
        {
            SubDomainCompose subDomainComposeEntity = _SubDomainComposeMapper.Map<SubDomainComposeModel, SubDomainCompose>(subDomainComposeModel);
            var subDomainCompose = _DAL.PostSubDomainCompose(subDomainComposeEntity);
            SubDomainComposeModel subDomainComposeModelReturn = _SubDomainComposeMapper.Map<SubDomainCompose, SubDomainComposeModel>(subDomainCompose);
            return subDomainComposeModelReturn;
        }


        public SubDomainComposeModel PatchSubDomainCompose(int id, JsonPatchDocument<SubDomainCompose> subDomainComposeModelJSON)
        {
            var subDomainComposeEntity = _DAL.PatchSubDomainCompose(id, subDomainComposeModelJSON);

            SubDomainComposeModel subDomainComposeModel = _SubDomainComposeMapper.Map<SubDomainCompose, SubDomainComposeModel>(subDomainComposeEntity);

            return subDomainComposeModel;
        }

        public SubDomainComposeModel PutSubDomainCompose(SubDomainComposeModel subDomainComposeModel)
        {
            SubDomainCompose subDomainComposeEntity = _SubDomainComposeMapper.Map<SubDomainComposeModel, SubDomainCompose>(subDomainComposeModel);
            var subDomainCompose = _DAL.PutSubDomainCompose(subDomainComposeEntity);
            SubDomainComposeModel subDomainComposeModelReturn = _SubDomainComposeMapper.Map<SubDomainCompose, SubDomainComposeModel>(subDomainCompose);
            return subDomainComposeModelReturn;
        }
        public void DeleteSubDomainCompose(int id)
        {
            _DAL.DeleteSubDomainCompose(id);
        }
    }
}


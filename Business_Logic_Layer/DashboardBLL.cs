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
    public class DashboardBLL : InterfaceDashboard
    {

        //private DashboardDAL _DAL;
        private Mapper _DashboardMapper;

    }
}


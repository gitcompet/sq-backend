using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Data_Access_Layer.Repository.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Linq;
using System.Security.Claims;

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    /*[Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles = "USER"
     )]*/
    [Route("api/v1/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly InterfaceDashboard _IDashboard;
        private readonly InterfaceQuizUser _IQuizUser;
        private readonly InterfaceElementTranslation _IElementTranslation;
        private static class TYPE_LABEL
        {
            public const string QUIZ_TITLE = "QUIZ_TITLE";
            public const string QUIZ_LABEL = "QUIZ_LABEL";
        }
        public DashboardController(InterfaceQuizUser interfaceQuizUser/*, InterfaceDashboard interfaceDashboard*/, InterfaceElementTranslation interfaceElementTranslation)
        {
            _IElementTranslation = interfaceElementTranslation;
            _IQuizUser = interfaceQuizUser;
            //_IDashboard = interfaceDashboard;
        }

        //GET api/v1/Dashboard/LastQuizDone
        [HttpGet]
        [Route("LastQuizDone")]
        public List<QuizUserModel> GetLastQuiz(int? amount)
        {
            int qty = 5;
            if (amount.HasValue)
            {
                qty = amount.Value;
            }

            return _IQuizUser.GetLastQuizValidates(qty);
        }

        //GET api/v1/Dashboard/PendingQuizes
        [HttpGet]
        [Route("PendingQuizes")]
        public List<QuizUserModel> PendingQuizes(int? amount)
        {
            int qty = 5;
            if (amount.HasValue)
            {
                qty = amount.Value;
            }

            return _IQuizUser.GetPendingQuizes(qty);
        }
    }
}


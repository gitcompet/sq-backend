using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;





namespace SkillLanguagezWebApi.Controllers
{
    [ApiController]
    /*[Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles = "ADMIN"
     )]*/
    [Route("api/v1/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly InterfaceLanguage _ILanguage;
        public LanguageController(InterfaceLanguage interfaceLanguage)
        {
            _ILanguage = interfaceLanguage;
        }

        //GET api/v1/Language
        [HttpGet]
        [Route("")]
        public List<LanguageModel> GetAllLanguage()
        {
            return _ILanguage.GetAllLanguage();
        }


        //GET api/v1/Language/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<LanguageModel> GetLanguageById(int id)
        {
            var language = _ILanguage.GetLanguageById(id);

            if (language == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(language);
        }

        //POST api/v1/Language
        [HttpPost]
        [Route("")]
        public ActionResult<LanguageModel> PostLanguage([FromBody] LanguageModelPostDTO languageModelPostDTO)
        {
            if (languageModelPostDTO != null)
            {
                var languageModel = new LanguageModel(languageModelPostDTO);
                var languageResult = _ILanguage.PostLanguage(languageModel);
                if (languageResult != null)
                {
                    return Created("/api/v1/Language/" + languageModel.LanguageId, languageResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/Language/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<LanguageModel> PatchLanguage([FromRoute] int id, [FromBody] JsonPatchDocument<Language> languageModelJSON)
        {
            if (languageModelJSON != null)
            {
                var language = _ILanguage.PatchLanguage(id, languageModelJSON);
                return Ok(language);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/Language
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<LanguageModel> PatchLanguage([FromRoute] int id, [FromBody] LanguageModel languageModel)
        {
            if (languageModel.LanguageId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (languageModel != null)
            {
                var language = _ILanguage.PutLanguage(languageModel);
                return Ok(language);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/Language/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<LanguageModel> DeleteLanguage([FromRoute] int id)
        {
            var language = _ILanguage.GetLanguageById(id);

            if (language == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _ILanguage.DeleteLanguage(id);
                return Ok(language);
            }

            //_ILanguage.DeleteLanguage(id);
        }

    }
}


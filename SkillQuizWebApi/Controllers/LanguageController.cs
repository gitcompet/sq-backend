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
        private readonly InterfaceElementTranslation _IElementTranslation;
        private static class TYPE_LABEL
        {
            public const string LABEL = "LANGUAGE_LABEL";
        }
        public LanguageController(InterfaceLanguage interfaceLanguage, InterfaceElementTranslation interfaceElementTranslation)
        {
            _IElementTranslation = interfaceElementTranslation;
            _ILanguage = interfaceLanguage;
        }

        //GET api/v1/Language
        [HttpGet]
        [Route("")]
        public List<LanguageModelLabel> GetAllLanguage()
        {
            var language = 2;
            var collection = _ILanguage.GetAllLanguage();
            List<LanguageModelLabel> result = new List<LanguageModelLabel>();
            foreach (var item in collection)
            {
                result.Add(new LanguageModelLabel(item, _IElementTranslation.GetElementLabelById(item.LanguageId.ToString(), TYPE_LABEL.LABEL, language)));
            }
            return result;
        }


        //GET api/v1/Language/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<LanguageModelLabel> GetLanguageById(int id)
        {
            var language = 2;
            var test = _ILanguage.GetLanguageById(id);
            if (test == null)
            {
                return NotFound("Invalid ID");
            }
            var result = new LanguageModelLabel(test, _IElementTranslation.GetElementLabelById(id.ToString(), TYPE_LABEL.LABEL, language));

            return Ok(result);
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


using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
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

namespace SkillElementTranslationzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ElementTranslationController : ControllerBase
    {
        private readonly InterfaceElementTranslation _IElementTranslation;
        public ElementTranslationController(InterfaceElementTranslation interfaceElementTranslation)
        {
            _IElementTranslation = interfaceElementTranslation;
        }

        //GET api/v1/ElementTranslation
        [HttpGet]
        [Route("")]
        public List<ElementTranslationModel> GetAllElementTranslation()
        {
            return _IElementTranslation.GetAllElementTranslation();
        }


        //GET api/v1/ElementTranslation/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<ElementTranslationModel> GetElementTranslationById(int id, string? elementType, int? languageId)
        {
            ElementTranslationModel elementTranslation;
            if (languageId.HasValue && elementType != null)
            {
                elementTranslation = _IElementTranslation.GetElementTranslationByKey(id, elementType, languageId.Value);
            }
            else
            {
                elementTranslation = _IElementTranslation.GetElementTranslationById(id);
            }

            if (elementTranslation == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(elementTranslation);
        }

        //POST api/v1/ElementTranslation
        [HttpPost]
        [Route("")]
        public ActionResult<ElementTranslationModel> PostElementTranslation([FromBody] ElementTranslationModelPostDTO elementTranslationModelPostDTO)
        {
            if (elementTranslationModelPostDTO != null)
            {
                var elementTranslationModel = new ElementTranslationModel(elementTranslationModelPostDTO);
                var elementTranslationResult = _IElementTranslation.PostElementTranslation(elementTranslationModel);
                if (elementTranslationResult != null)
                {
                    return Created("/api/v1/ElementTranslation/" + elementTranslationModel.ElementTranslationId, elementTranslationResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/ElementTranslation/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<ElementTranslationModel> PatchElementTranslation([FromRoute] int id, [FromBody] JsonPatchDocument<ElementTranslation> elementTranslationModelJSON)
        {
            if (elementTranslationModelJSON != null)
            {
                var elementTranslation = _IElementTranslation.PatchElementTranslation(id, elementTranslationModelJSON);
                return Ok(elementTranslation);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/ElementTranslation
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<ElementTranslationModel> PutElementTranslation([FromRoute] int id, [FromBody] ElementTranslationModel elementTranslationModel)
        {
            if (elementTranslationModel.ElementTranslationId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (elementTranslationModel != null)
            {
                var elementTranslation = _IElementTranslation.PutElementTranslation(elementTranslationModel);
                return Ok(elementTranslation);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/ElementTranslation/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<ElementTranslationModel> DeleteElementTranslation([FromRoute] int id)
        {
            var elementTranslation = _IElementTranslation.GetElementTranslationById(id);

            if (elementTranslation == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IElementTranslation.DeleteElementTranslation(id);
                return Ok(elementTranslation);
            }
        }
    }
}


using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceElementTranslation
    {

        List<ElementTranslationModel> GetAllElementTranslation();
        ElementTranslationModel GetElementTranslationById(int id);
        ElementTranslationModel PostElementTranslation(ElementTranslationModel elementTranslationModel);
        ElementTranslationModel PatchElementTranslation(int id, JsonPatchDocument<ElementTranslation> elementTranslationModelJSON);
        ElementTranslationModel PutElementTranslation(ElementTranslationModel elementTranslationModel);
        void DeleteElementTranslation(int id);
    }
}

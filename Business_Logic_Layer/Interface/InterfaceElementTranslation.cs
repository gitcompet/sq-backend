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

        string GetElementLabelById(string id, string elementType, int languageId);
        IEnumerable<string> GetElementsLabelById(IEnumerable<string> id, string elementType, int languageId);
        List<ElementTranslationModel> GetAllElementTranslation();
        ElementTranslationModel GetElementTranslationById(int id);
        ElementTranslationModel GetElementTranslationByKey(int id, string elementType, int languageId);
        ElementTranslationModel PostElementTranslation(ElementTranslationModel elementTranslationModel);
        ElementTranslationModel PatchElementTranslation(int id, JsonPatchDocument<ElementTranslation> elementTranslationModelJSON);
        ElementTranslationModel PutElementTranslation(ElementTranslationModel elementTranslationModel);
        void DeleteElementTranslation(int id);
        void DeleteElementTranslationByItem(int id, string type);
    }
}

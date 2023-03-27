using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceLanguage
    {

        List<LanguageModel> GetAllLanguage();
        LanguageModel GetLanguageById(int id);
        LanguageModel PostLanguage(LanguageModel languageModel);
        LanguageModel PatchLanguage(int id, JsonPatchDocument<Language> languageModelJSON);
        LanguageModel PutLanguage(LanguageModel languageModel);
        void DeleteLanguage(int id);
    }
}

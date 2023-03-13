using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceLanguages
    {

        List<LanguagesModel> GetAllLanguages();


        LanguagesModel GetLanguagesById(int id);
        int PostLanguages(LanguagesModelDTO languagesModelDTO);
    }
}

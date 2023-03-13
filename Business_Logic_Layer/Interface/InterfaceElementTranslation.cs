using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Interface
{
    public interface InterfaceElementTranslation
    {
            List<ElementTranslationModel> GetAllElementTranslation();


            ElementTranslationModel GetElementTranslationById(int id);
            void PostElementTranslation(ElementTranslationModel elementtranslationModel);
        }
    }

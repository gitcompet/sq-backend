using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class ElementTranslationModel
    {
        public ElementTranslationModel ()
        {

        }
        public ElementTranslationModel (ElementTranslationModelPostDTO elementTranslationModelPostDTO)
        {
            this.ElementId = elementTranslationModelPostDTO.ElementId;
            this.ElementType = elementTranslationModelPostDTO.ElementType;
            this.LanguagesId = elementTranslationModelPostDTO.LanguagesId;
            if (elementTranslationModelPostDTO.Description != null)
            {
                this.Description = elementTranslationModelPostDTO.Description;
            }
            else
            {
                this.Description = "";
            }
        }
        public String ElementTranslationId { get; set; }
        public int ElementId { get; set; }
        public String ElementType { get; set; }
        public int LanguagesId { get; set; }
        public String Description { get; set; }
    }
}

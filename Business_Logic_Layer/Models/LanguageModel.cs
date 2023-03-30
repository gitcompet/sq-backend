using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class LanguageModel
    {
        public LanguageModel(LanguageModelPostDTO languageModelPostDTO)
        {

            this.title = languageModelPostDTO.title;
            this.shortCode = languageModelPostDTO.shortCode;
            this.isActive = true;
            this.isDeleted = false;
            this.isAppRelated = languageModelPostDTO.isAppRelated;
            this.isTestRelated = languageModelPostDTO.isTestRelated;
        }
        public LanguageModel() { }
        public string LanguageId { get; set; }
        public string title { get; set; }
        public string shortCode { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isAppRelated { get; set; }
        public Boolean isTestRelated { get; set; }
        public Boolean isDeleted { get; set; }
    }
}

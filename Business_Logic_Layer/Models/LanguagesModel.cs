using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class LanguagesModel
    {
        public LanguagesModel(LanguagesModelDTO languagesModelDTO)
        {

            this.title = languagesModelDTO.title;
            this.shortCode = languagesModelDTO.shortCode;
            this.isActive = true;
            this.isDeleted = false;
            this.isAppRelated = false;
            this.isTestRelated = false;
        }
        public LanguagesModel() { }
        public String LanguagesId { get; set; }
        public string title { get; set; }
        public string shortCode { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isAppRelated { get; set; }
        public Boolean isTestRelated { get; set; }
        public Boolean isDeleted { get; set; }
    }
}

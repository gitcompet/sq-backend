using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class LanguageModelLabel
    {
        public LanguageModelLabel(LanguageModel languageModel, string label)
        {
            this.LanguageId = languageModel.LanguageId;
            this.title = languageModel.title;
            this.shortCode = languageModel.shortCode;
            this.isActive = languageModel.isActive;
            this.isAppRelated = languageModel.isAppRelated;
            this.isTestRelated = languageModel.isTestRelated;
            this.isDeleted = languageModel.isDeleted;
            this.Label = label;
        }
        public string LanguageId { get; set; }
        public string title { get; set; }
        public string shortCode { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isAppRelated { get; set; }
        public Boolean isTestRelated { get; set; }
        public Boolean isDeleted { get; set; }
        public string Label { get; set; }
    }
}

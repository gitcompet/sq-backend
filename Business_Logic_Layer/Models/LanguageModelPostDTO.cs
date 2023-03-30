using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class LanguageModelPostDTO
    {
        public LanguageModelPostDTO(){}
        public string title { get; set; }
        public string shortCode { get; set; }
        public Boolean isAppRelated { get; set; }
        public Boolean isTestRelated { get; set; }
    }
}

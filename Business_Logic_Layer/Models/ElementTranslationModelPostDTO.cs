using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class ElementTranslationModelPostDTO
    {
        public int ElementId { get; set; }
        public string ElementType { get; set; }
        public int LanguagesId { get; set; }
        public string Description { get; set; }
    }
}

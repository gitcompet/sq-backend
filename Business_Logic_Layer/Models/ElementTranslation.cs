using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class ElementTranslationModel
    {
        public String ElementTranslationId { get; set; }
        public int ElementId { get; set; }
        public String ElementType { get; set; }
        public int LanguagesId { get; set; }
        public String Description { get; set; }
    }
}

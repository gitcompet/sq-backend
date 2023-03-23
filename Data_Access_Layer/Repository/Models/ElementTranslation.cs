using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class ElementTranslation
    {
        public int ElementTranslationId { get; set; }
        public int ElementId { get; set; }
        public string ElementType { get; set; }
        public int LanguagesId { get; set; }
        public string Description { get; set; }
    }
}

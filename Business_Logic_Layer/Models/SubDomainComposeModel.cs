using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class SubDomainComposeModel
    {
        public SubDomainComposeModel() { }
        public SubDomainComposeModel(SubDomainComposeModelPostDTO subDomainComposeModelPostDTO)
        {
            this.SubDomainId = subDomainComposeModelPostDTO.SubDomainId;
            this.ElementType = subDomainComposeModelPostDTO.ElementType;
            this.ElementId = subDomainComposeModelPostDTO.ElementId;
        }
        public string SubDomainComposeId { get; set; }
        public string ElementId { get; set; }
        public string SubDomainId { get; set; }
        public string ElementType { get; set; }
    }
}

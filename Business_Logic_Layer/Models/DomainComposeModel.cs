using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class DomainComposeModel
    {
        public DomainComposeModel() { }
        public DomainComposeModel(DomainComposeModelPostDTO domainComposeModelPostDTO)
        {
            this.DomainId = domainComposeModelPostDTO.DomainId;
            this.SubDomainid = domainComposeModelPostDTO.SubDomainid;
        }
        public string DomainComposeId { get; set; }
        public string DomainId { get; set; }
        public string SubDomainid { get; set; }
    }
}

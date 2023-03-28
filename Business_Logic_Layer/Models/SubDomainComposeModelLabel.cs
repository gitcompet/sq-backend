using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class SubDomainComposeModelLabel
    {
        public SubDomainComposeModelLabel(string id, IEnumerable<string> doms, IEnumerable<string> domNames)
        {
            this.ElementId = id;
            this.SubDomainNames = domNames;
            this.SubDomainId = doms;
        }
        public string ElementId { get; set; }
        public IEnumerable<string> SubDomainId { get; set; }
        public IEnumerable<string> SubDomainNames { get; set; }
    }
}

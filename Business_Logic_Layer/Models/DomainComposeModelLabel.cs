using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class DomainComposeModelLabel
    {
        public DomainComposeModelLabel(string id, IEnumerable<string> doms, IEnumerable<string> domNames)
        {
            this.ElementId = id;
            this.DomainNames = domNames;
            this.DomainId = doms;
        }
        public string ElementId { get; set; }
        public IEnumerable<string> DomainId { get; set; }
        public IEnumerable<string> DomainNames { get; set; }
    }
}

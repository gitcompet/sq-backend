using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class SubDomainModelLabel
    {
        public SubDomainModelLabel() { }
        public SubDomainModelLabel(SubDomainModel subDomainModel, string title)
        {
            this.SubDomainId = subDomainModel.SubDomainId;
            this.Name = subDomainModel.Name;
            this.isActive = subDomainModel.isActive;
            this.isDeleted = subDomainModel.isDeleted;
            this.Title = title;
        }
        public string SubDomainId { get; set; }
        public string Name { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
        public string Title;
    }
}

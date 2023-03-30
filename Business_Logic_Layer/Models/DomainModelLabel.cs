using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class DomainModelLabel
    {
        public DomainModelLabel() { }
        public DomainModelLabel(DomainModel domainModel, string title)
        {
            this.DomainId = domainModel.DomainId;
            this.Name = domainModel.Name;
            this.isActive = domainModel.isActive;
            this.isDeleted = domainModel.isDeleted;
            this.Title = title;
        }
        public string DomainId { get; set; }
        public string Name { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
        public string Title { get; set; }
    }
}

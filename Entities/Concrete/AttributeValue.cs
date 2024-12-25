using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class AttributeValue:BaseEntity
    {
        public int AttributeId { get; set; }
        public string Value { get; set; } 


        public virtual Attribute Attribute { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }    
    }
}

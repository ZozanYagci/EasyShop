using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Attribute:BaseEntity
    {
        public string Name { get; set; } // Renk, Beden, Materyal vs


        public virtual ICollection<AttributeValue> AttributeValues { get; set; }
    }
}

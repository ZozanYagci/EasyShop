using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductAttribute : BaseEntity
    {
        public int ProductId { get; set; }
        public int AttributeValueId { get; set; }


        public Product Product { get; set; }
        public virtual AttributeValue AttributeValue { get; set; }

    }
}

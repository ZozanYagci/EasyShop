using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductDetailType:BaseEntity
    {
        public string Title { get; set; } //Description, Info

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}

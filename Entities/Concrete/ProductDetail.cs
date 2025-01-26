using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductDetail:BaseEntity
    {
        public int ProductId { get; set; }
        public int ProductDetailTypeId { get; set; }
        public string Content { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductDetailType ProductDetailType { get; set; }

    }
}

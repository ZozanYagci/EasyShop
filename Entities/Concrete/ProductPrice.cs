using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductPrice:BaseEntity
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public bool IsCurrent { get;set; }
        public DateTime EffectiveDate { get; set; }

        public virtual Product Product { get; set; }
    }
}

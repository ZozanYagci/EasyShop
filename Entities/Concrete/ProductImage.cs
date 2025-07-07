using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; } // vitrin resmi
        public int SortOrder { get; set; } //çoklu resim sırası
        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; }


    }
}

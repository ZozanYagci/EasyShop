using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public int SubCategoryId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
    }
}

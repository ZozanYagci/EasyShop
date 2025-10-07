using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ShoppingCart : BaseEntity
    {
        public int AuthUserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public virtual AuthUser AuthUser { get; set; }
        public virtual ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    }
}

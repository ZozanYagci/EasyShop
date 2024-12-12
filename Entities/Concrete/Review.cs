using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Review:BaseEntity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; } // 1-5 arası - yorum puanı
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }  
        public virtual Product Product { get; set; }
    }
}

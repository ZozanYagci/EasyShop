using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class CartMessages
    {
        public const string ProductAddedToNewCart = "İlk ürün sepete eklendi, sepetiniz oluşturuldu.";
        public const string ProductAddedToCart = "Ürün sepete eklendi.";
        public const string CartNotFound = "Sepet bulunamadı.";
        public const string ProductNotFoundInCart = "Ürün sepette bulunamadı.";
        public const string CartIsEmpty = "Sepetiniz boş";
        public const string CartQuantityUpdated = "Sepetteki ürün adedi güncellendi.";
        public const string ProductRemovedFromCart = "Ürün sepetten kaldırıldı.";
        public const string CartCleared = "Sepet başarıyla temizlendi.";

    }
}

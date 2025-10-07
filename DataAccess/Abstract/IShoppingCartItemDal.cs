using Core.DataAccess.EntityFramework;
using Entities.Concrete;


namespace DataAccess.Abstract
{
    public interface IShoppingCartItemDal : IGenericRepository<ShoppingCartItem>
    {
        Task<ShoppingCartItem?> GetCartItemAsync(int userId, int productId);
        Task<List<ShoppingCartItem>> GetItemsByCartIdAsync(int cartId);
    }
}

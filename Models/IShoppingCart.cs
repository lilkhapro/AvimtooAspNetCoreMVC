namespace WebAppVide.Models
{
    public interface IShoppingCart
    {
        List<ShoppingCartItem> ShoppingCartItems { get; set; }

        void ClearCart();
        void AddToCart(Pie pie);
        int RemoveFromCart(Pie pie);
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> GetShoppingCartItems();
    }
}

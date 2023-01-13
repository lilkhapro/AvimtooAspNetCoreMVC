using Microsoft.EntityFrameworkCore;

namespace WebAppVide.Models
{
    public class ShoppingCart : IShoppingCart
    {
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        private readonly WebAppPieDbContext _webAppPieDbContext;
        public string? ShoppingCartId { get; set; }

        private ShoppingCart(WebAppPieDbContext webAppPieDbContext)
        {
            _webAppPieDbContext= webAppPieDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>
                ()?.HttpContext?.Session;

            WebAppPieDbContext context = services.GetService<WebAppPieDbContext>
                () ?? throw new Exception("Error initializing");

            string cartid = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartid);

            return new ShoppingCart(context) { ShoppingCartId = cartid };
        }
        public void AddToCart(Pie pie)
        {
            var shoppingCartItem = _webAppPieDbContext.ShoppingCartItems
                .SingleOrDefault(s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == 
                ShoppingCartId);
            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                _webAppPieDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _webAppPieDbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _webAppPieDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _webAppPieDbContext.ShoppingCartItems.RemoveRange(cartItems);
            _webAppPieDbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                _webAppPieDbContext.ShoppingCartItems.Where(
                    c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Pie)
                .ToList();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _webAppPieDbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == this.ShoppingCartId)
                .Select(c => c.Amount * c.Pie.Price)
                .Sum();

            return total;
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
    _webAppPieDbContext.ShoppingCartItems.SingleOrDefault(
        s => s.Pie.PieId == pie.PieId && ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _webAppPieDbContext.ShoppingCartItems
                        .Remove(shoppingCartItem);
                }
            }

            _webAppPieDbContext.SaveChanges();

            return localAmount;
        }
    }
}

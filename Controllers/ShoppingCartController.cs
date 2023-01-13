using Microsoft.AspNetCore.Mvc;
using WebAppVide.Models;
using WebAppVide.ViewModels;

namespace WebAppVide.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartController(IPieRepository pieRepository, IShoppingCart shoppingCart)
        {
            this._pieRepository = pieRepository;
            this._shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var paniers = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = paniers;

            var shoppingCartViewModal = new ShoppingCartViewModel(_shoppingCart,
                _shoppingCart.GetShoppingCartTotal());
            return View(shoppingCartViewModal);
        }
        
        public RedirectToActionResult AddToShoppingCart(int PieId)
        {
            var selectedPie = _pieRepository.AllPies
                .FirstOrDefault(p => p.PieId == PieId);
            if (selectedPie !=  null)
            {
                _shoppingCart.AddToCart(selectedPie);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int PiedId)
        {
            var selected = _pieRepository.AllPies
                .FirstOrDefault(p => p.PieId == PiedId);

            if(selected != null)
            {
                _shoppingCart.RemoveFromCart(selected); 
            } 

            return RedirectToAction("Index");
        }

    }
}

using System.Web.Mvc;
using TimeWarpGames.Bll;
using TimeWarpGames.Dal;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Webapp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartBll _cartBll;

        public ShoppingCartController()
        {
            _cartBll = new ShoppingCartBll(Session);
        }

        public ActionResult Index()
        {
            var cart = _cartBll.GetCart();
            return View(cart);
        }

        [HttpPost]
        public ActionResult Add(int productId)
        {
            var product = ProductDal.ReadOne(productId); // Of AccessoryDal/ConsoleDal afhankelijk van je structuur

            if (product == null || product.Stock <= 0)
            {
                TempData["ErrorMessage"] = "Product niet beschikbaar.";
                return RedirectToAction("Index", "Home");
            }

            _cartBll.AddProduct(new ShoppingCartItem(product.ProductId, product.Name, 1, product.Price));
            TempData["SuccessMessage"] = $"{product.Name} toegevoegd aan je winkelmandje.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Remove(int productId)
        {
            _cartBll.RemoveProduct(productId);
            TempData["SuccessMessage"] = "Product verwijderd uit winkelmandje.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int productId, int quantity)
        {
            _cartBll.UpdateQuantity(productId, quantity);
            TempData["SuccessMessage"] = "Aantal aangepast.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Clear()
        {
            _cartBll.ClearCart();
            TempData["SuccessMessage"] = "Winkelmandje geleegd.";
            return RedirectToAction("Index");
        }
    }
}
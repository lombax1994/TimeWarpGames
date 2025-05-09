using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeWarpGames.Bll;
using TimeWarpGames.Entities;
using Console = TimeWarpGames.Entities.Console;

namespace TimeWarpGames.Webapp.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = Session["ShoppingCart"] as List<ShoppingCartItem> ?? new List<ShoppingCartItem>();
            ViewBag.CartCount = cart.Sum(i => i.Quantity);
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(int productId)
        {
            var product = ProductBll.ReadAll().FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
                return HttpNotFound();

            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                if (existingItem.Quantity >= product.Stock)
                {
                    TempData["Error"] = "Niet genoeg voorraad beschikbaar.";
                    return RedirectToProductDetails(product);
                }

                existingItem.Quantity++;
            }
            else
            {
                if (product.Stock < 1)
                {
                    TempData["Error"] = "Niet op voorraad.";
                    return RedirectToProductDetails(product);
                }

                cart.Add(new ShoppingCartItem(product.ProductId, product.Name, 1, product.Price));
            }

            SaveCart(cart);
            return RedirectToProductDetails(product);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                return RedirectToAction("Index");

            var product = ProductBll.ReadAll().FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
                return RedirectToAction("Index");

            if (quantity < 1)
            {
                cart.Remove(item);
            }
            else if (quantity <= product.Stock)
            {
                item.Quantity = quantity;
            }
            else
            {
                TempData["Error"] = $"Er zijn slechts {product.Stock} stuks beschikbaar van dit product.";
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        //hulpfuncties voor het beheren van het winkelwagentje
        //en het correct doorverwijzen naar de juiste detailspagina
        private List<ShoppingCartItem> GetCart()
        {
            var cart = Session["ShoppingCart"] as List<ShoppingCartItem>;
            if (cart == null)
            {
                cart = new List<ShoppingCartItem>();
                Session["ShoppingCart"] = cart;
            }
            return cart;
        }

        private void SaveCart(List<ShoppingCartItem> cart)
        {
            Session["ShoppingCart"] = cart;
        }

        private ActionResult RedirectToProductDetails(Product product)
        {
            if (product is Console)
                return RedirectToAction("Details", "Consoles", new { id = product.ProductId });
            if (product is Game)
                return RedirectToAction("Details", "Games", new { id = product.ProductId });
            if (product is Accessory)
                return RedirectToAction("Details", "Accessories", new { id = product.ProductId });

            return RedirectToAction("Index", "Products"); // fallback
        }
    }
}
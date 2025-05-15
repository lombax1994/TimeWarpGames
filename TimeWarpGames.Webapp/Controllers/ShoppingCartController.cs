using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TimeWarpGames.Bll;
using TimeWarpGames.Entities;

public class ShoppingCartController : BaseController
{
    // GET: ShoppingCart - We tonen de inhoud van het winkelwagentje.
    public ActionResult Index()
    {
        // Haal de winkelwagen op uit de sessie
        var cart = GetCart();

        // Toon het aantal producten in de winkelwagen via ViewBag (bijv. voor weergave in de navigatie)
        ViewBag.CartCount = cart.Sum(i => i.Quantity);

        // Return de winkelwagen als model voor de view
        return View(cart); // De winkelwagen wordt weergegeven in de Index view
    }

    [HttpPost]
    [Authorize]
    public ActionResult Add(int productId)
    {
        var product = ProductBll.ReadAll().FirstOrDefault(p => p.ProductId == productId);
        if (product == null)
        {
            if (Request.IsAjaxRequest())
                return Json(new { success = false, message = "Product niet gevonden." });

            return HttpNotFound();
        }

        var cart = GetCart();
        var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            if (existingItem.Quantity >= existingItem.Stock)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { success = false, message = "Niet genoeg voorraad beschikbaar." });

                TempData["Error"] = "Niet genoeg voorraad beschikbaar.";
                return RedirectToProductDetails(product);
            }

            existingItem.Quantity++;
        }
        else
        {
            if (product.Stock < 1)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { success = false, message = "Niet op voorraad." });

                TempData["Error"] = "Niet op voorraad.";
                return RedirectToProductDetails(product);
            }

            cart.Add(new ShoppingCartItem(product.ProductId, product.Name, 1, product.Price, product.Stock));
        }

        SaveCart(cart);

        if (Request.IsAjaxRequest())
            return Json(new { success = true });

        return RedirectToProductDetails(product);
    }

    // POST: Update item quantity in cart - Deze actie werkt de hoeveelheid van een product bij in de winkelwagen
    [HttpPost]
    [Authorize] // Zorgt ervoor dat de gebruiker ingelogd moet zijn om iets bij te werken
    public ActionResult UpdateQuantity(int productId, int quantity)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(i => i.ProductId == productId);
        if (item == null)
            return RedirectToAction("Index");

        var product = ProductBll.ReadAll().FirstOrDefault(p => p.ProductId == productId);
        if (product == null)
            return RedirectToAction("Index");

        // Gebruik de voorraad van het product in plaats van de Stock van ShoppingCartItem
        if (quantity < 1)
        {
            cart.Remove(item);
        }
        else if (quantity <= item.Stock) // Controleer tegen de voorraad die in de ShoppingCartItem staat
        {
            item.Quantity = quantity;
        }
        else
        {
            TempData["Error"] = $"Er zijn slechts {item.Stock} stuks beschikbaar van dit product."; // Foutmelding bij onvoldoende voorraad
        }

        SaveCart(cart);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize]
    public ActionResult Remove(int productId)
    {
        // Haal de winkelwagen op uit de sessie
        var cart = GetCart();

        // Zoek het item in de winkelwagen
        var itemToRemove = cart.FirstOrDefault(i => i.ProductId == productId);

        if (itemToRemove != null)
        {
            // Verwijder het item uit de winkelwagen
            cart.Remove(itemToRemove);
            // Bewaar de bijgewerkte winkelwagen
            SaveCart(cart);
        }

        // Redirect naar de winkelwagen indexpagina na verwijderen
        return RedirectToAction("Index");
    }

    // Helper methods for managing cart

    // Haal de winkelwagen op uit de sessie, maak deze aan als hij niet bestaat
    private List<ShoppingCartItem> GetCart()
    {
        var cart = Session["ShoppingCart"] as List<ShoppingCartItem>;
        if (cart == null)
        {
            cart = new List<ShoppingCartItem>();
            Session["ShoppingCart"] = cart; // Sla de nieuwe lege winkelwagen op in de sessie
        }
        return cart; // Return de winkelwagen
    }

    // Sla de winkelwagen op in de sessie
    private void SaveCart(List<ShoppingCartItem> cart)
    {
        Session["ShoppingCart"] = cart; // Bewaar de winkelwagen in de sessie
    }

    // Helper om door te sturen naar de juiste productdetailpagina
    private ActionResult RedirectToProductDetails(Product product)
    {
        if (product is Console)
            return RedirectToAction("Details", "Consoles", new { id = product.ProductId });
        if (product is Game)
            return RedirectToAction("Details", "Games", new { id = product.ProductId });
        if (product is Accessory)
            return RedirectToAction("Details", "Accessories", new { id = product.ProductId });

        return RedirectToAction("Index", "Home"); // fallback naar Home pagina
    }
    
}

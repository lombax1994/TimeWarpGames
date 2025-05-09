using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TimeWarpGames.Bll;
using TimeWarpGames.Entities;

public class ShoppingCartController : Controller
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

    // POST: Add item to cart - Deze actie voegt een product toe aan de winkelwagen
    [HttpPost]
    [Authorize] // Zorgt ervoor dat de gebruiker ingelogd moet zijn om iets toe te voegen
    public ActionResult Add(int productId)
    {
        // Zoek het product op basis van de meegegeven productId
        var product = ProductBll.ReadAll().FirstOrDefault(p => p.ProductId == productId);
        if (product == null)
            return HttpNotFound(); // Als het product niet gevonden is, geef een 404 fout

        // Haal de huidige winkelwagen op
        var cart = GetCart();

        // Zoek of het product al in de winkelwagen zit
        var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            // Als het product al in de winkelwagen zit, verhoog de hoeveelheid
            if (existingItem.Quantity >= existingItem.Stock)
            {
                TempData["Error"] = "Niet genoeg voorraad beschikbaar."; // Geef foutmelding bij onvoldoende voorraad
                return RedirectToProductDetails(product); // Redirect naar de productdetailpagina
            }

            existingItem.Quantity++; // Verhoog de hoeveelheid van het product in de winkelwagen
        }
        else
        {
            // Als het product nog niet in de winkelwagen zit, voeg het toe
            if (product.Stock < 1)
            {
                TempData["Error"] = "Niet op voorraad."; // Foutmelding als het product niet op voorraad is
                return RedirectToProductDetails(product); // Redirect naar de productdetailpagina
            }

            // Voeg het product toe aan de winkelwagen, met de voorraad van het product
            cart.Add(new ShoppingCartItem(product.ProductId, product.Name, 1, product.Price, product.Stock));
        }

        // Sla de winkelwagen op in de sessie
        SaveCart(cart);

        // Redirect de gebruiker terug naar de productdetailpagina
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

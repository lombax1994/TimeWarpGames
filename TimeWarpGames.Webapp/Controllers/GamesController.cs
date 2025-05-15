using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TimeWarpGames.Bll;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Webapp.Controllers
{
    public class GamesController : BaseController
    {
        public ActionResult Index()
        {
            try
            {
                List<Game> lstGames = GameBll.ReadAll();
                return View(lstGames);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Er is een fout opgetreden bij het ophalen van de spellen.";
                return View("Error");
            }
        }

        [Authorize(Roles = "StoreManager")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name, bool IsBoxed, HttpPostedFileBase Image, string Description,
            decimal Price, int Stock, Platform Platform, Genre Genre, string Developer, DateTime ReleaseDate, int AgeRating)
        {
            string ImageName = "~/Content/Images/placeholder.png";

            if (Image != null)
            {
                if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png")
                {
                    string pathToSave = Server.MapPath("~/Content/Images/GamePics/");
                    string extension = Path.GetExtension(Image.FileName);
                    ImageName = Guid.NewGuid() + extension;
                    string fullPath = Path.Combine(pathToSave, ImageName);
                    Image.SaveAs(fullPath);
                }
            }

            bool gameCreated = GameBll.Create(Name, IsBoxed, ImageName, Description, Price, Stock,
                Platform, Genre, Developer, ReleaseDate, AgeRating);

            if (gameCreated)
            {
                TempData["Feedback"] = $"{Name} is aangemaakt.";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Feedback = "Aanmaken van spel is niet gelukt.";
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var game = GameBll.ReadOne(id);
                if (game == null) return View("Error");

                var cartBll = new ShoppingCartBll(Session);
                int quantityInCart = cartBll.GetQuantityInCart(game.ProductId);

                int availableStock = game.Stock - quantityInCart;
                if (availableStock < 0) availableStock = 0;

                ViewBag.ActueleVoorraad = availableStock;

                return View(game);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        [Authorize(Roles = "StoreManager")]
        public ActionResult Delete(int id)
        {
            try
            {
                var game = GameBll.ReadOne(id);
                if (game == null) return View("Error");

                return View(game);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Er is een fout opgetreden bij het ophalen van de spelgegevens.";
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            int gameId = Convert.ToInt32(id);
            bool gameDeleted = GameBll.Delete(gameId);

            if (gameDeleted)
            {
                TempData["Feedback"] = "Game is verwijderd.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        [Authorize(Roles = "StoreManager")]
        public ActionResult Edit(int id)
        {
            try
            {
                var game = GameBll.ReadOne(id);
                if (game == null) return View("Error");

                return View(game);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, string Name, bool IsBoxed, HttpPostedFileBase Image, string Description,
            decimal Price, int Stock, Platform Platform, Genre Genre, string Developer, DateTime ReleaseDate, int AgeRating)
        {
            var existingGame = GameBll.ReadOne(id);
            if (existingGame == null) return View("Error");

            string ImageName = existingGame.Image;

            if (Image != null)
            {
                if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png")
                {
                    string pathToSave = Server.MapPath("~/Content/Images/GamePics/");
                    string extension = Path.GetExtension(Image.FileName);
                    ImageName = Guid.NewGuid() + extension;
                    string fullPath = Path.Combine(pathToSave, ImageName);
                    Image.SaveAs(fullPath);
                }
            }

            bool gameUpdated = GameBll.Update(id, Name, IsBoxed, ImageName, Description, Price, Stock,
                Platform, Genre, Developer, ReleaseDate, AgeRating);

            if (gameUpdated)
            {
                TempData["Feedback"] = $"{Name} is bijgewerkt.";
                return RedirectToAction("Details", new { id });
            }
            else
            {
                ViewBag.Feedback = "Bijwerken van spel is niet gelukt.";
                return View(existingGame);
            }
        }
    }
}

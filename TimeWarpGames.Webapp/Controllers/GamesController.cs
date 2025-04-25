using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TimeWarpGames.Bll;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Webapp.Controllers
{
    public class GamesController : Controller
    {
        // GET: Games
        public ActionResult Index()
        {
            try
            {
                List<Game> lstGames = TimeWarpGames.Bll.GameBll.ReadAll();
                return View(lstGames);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                ViewBag.ErrorMessage = "Er is een fout opgetreden bij het ophalen van de spellen.";
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name, bool IsBoxed, HttpPostedFileBase Image, string Description,
            decimal Price,
            int Stock,
            Platform Platform, Genre Genre, string Developer, DateTime ReleaseDate, int AgeRating)
        {
            string ImageName = "~/Content/Images/placeholder.png";

            if (Image != null)
            {
                if (Image.ContentType == "image/jpeg" || Image.ContentType == "image.png")
                {
                    string pathToSave = Server.MapPath("~/Content/Images/GamePics/");
                    string ImageExtension = Path.GetExtension(Image.FileName);
                    ImageName = Guid.NewGuid() + ImageExtension;
                    pathToSave += ImageName;
                    Image.SaveAs(pathToSave);
                }
            }

            bool memberCreated = TimeWarpGames.Bll.GameBll.Create(Name, IsBoxed, ImageName, Description, Price, Stock,
                Platform, Genre, Developer, ReleaseDate, AgeRating);
            if (memberCreated)
            {
                ViewBag.Feedback = Name + " is aangemaakt";
            }
            else
            {
                ViewBag.Feedback = "Aanmaken van spel is niet gelukt";
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            try
            {
                Game game = TimeWarpGames.Bll.GameBll.ReadOne(id);
                return View(game);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Er is een fout opgetreden bij het ophalen van de spelgegevens.";
                return View("Error");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Game game = TimeWarpGames.Bll.GameBll.ReadOne(id);
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

        // GET: Games/Edit/5
        // GET: Games/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Game game = TimeWarpGames.Bll.GameBll.ReadOne(id);
                if (game == null)
                {
                    // If no game is found with the given id, return HttpNotFound
                    return HttpNotFound();
                }

                return View(game);
            }
            catch (Exception ex)
            {
                // In case of any errors (e.g., DB connection failure), show an error page
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }


        // POST: Games/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string Name, bool IsBoxed, HttpPostedFileBase Image, string Description,
            decimal Price, int Stock, Platform Platform, Genre Genre, string Developer, DateTime ReleaseDate, int AgeRating)
        {
            string ImageName;

            // Retrieve the existing game
            Game existingGame = TimeWarpGames.Bll.GameBll.ReadOne(id);
            if (existingGame == null)
            {
                return HttpNotFound(); // Return 404 if the game doesn't exist
            }

            ImageName = existingGame.Image; // Keep the existing image if not replaced

            // If a new image is uploaded, handle the file saving
            if (Image != null)
            {
                if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png")
                {
                    string pathToSave = Server.MapPath("~/Content/Images/GamePics/");
                    string ImageExtension = Path.GetExtension(Image.FileName);
                    ImageName = Guid.NewGuid() + ImageExtension;
                    Image.SaveAs(Path.Combine(pathToSave, ImageName));
                }
            }

            // Call the BLL to update the game
            bool gameUpdated = TimeWarpGames.Bll.GameBll.Update(id, Name, IsBoxed, ImageName, Description, Price, Stock,
                Platform, Genre, Developer, ReleaseDate, AgeRating);

            if (gameUpdated)
            {
                ViewBag.Feedback = Name + " is bijgewerkt";
            }
            else
            {
                ViewBag.Feedback = "Bijwerken van spel is niet gelukt";
            }

            return View(TimeWarpGames.Bll.GameBll.ReadOne(id)); // Re-fetch and display the updated game
        }




    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using TimeWarpGames.Bll;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Webapp.Controllers
{
    public class GamesController : Controller
    {
        //met de index methode maken we een lijst van games aan om weer te geven in de view
        public ActionResult Index()
        {
            try
            {
                //We vragen de games op bij de bll
                List<Game> lstGames = TimeWarpGames.Bll.GameBll.ReadAll();
                return View(lstGames);
            }
            catch (Exception ex)
            {
                //Als er problemen zijn dan gaan we naar de errorpagina.
                ViewBag.ErrorMessage = "Er is een fout opgetreden bij het ophalen van de spellen.";
                return View("Error");
            }
        }

        [Authorize(Roles = "StoreManager")]
        public ActionResult Create()
        {
            // Deze create methode maakt enkel de Create view aan, hier kunnen we gegevens invullen
            return View();
        }


        //Dit is de effectieve create methode
        //Hier worden de gegevens die we hebben ingevuld in de create view naar de database gestuurd
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

        //Met de details methode maken we een view van een game aan
        public ActionResult Details(int id)
        {
            //We vragen de game op bij de bll
            try
            {
                Game game = TimeWarpGames.Bll.GameBll.ReadOne(id);
                return View(game);
            }
            //Als er problemen zijn dan gaan we naar de errorpagina.
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Er is een fout opgetreden bij het ophalen van de spelgegevens.";
                return View("Error");
            }
        }

        //Met de delete methode maken we een view van een game aan
        //Hier kunnen we de game verwijderen
        [Authorize(Roles = "StoreManager")]
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


        //Dit is de effectieve delete methode
        //Als je op de delete view op de delete knop drukt, dan wordt deze methode aangeroepen
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

        //Hier wordt de edit view aangemaakt
        [Authorize(Roles = "StoreManager")]
        public ActionResult Edit(int id)
        {
            try
            {
                Game game = TimeWarpGames.Bll.GameBll.ReadOne(id);
                if (game == null)
                {
                    //als er geen spel wordt gevonden gaan we naar de errorpagina
                    return View("Error");
                }

                // We geven de game door aan de view
                return View(game);
            }
            catch (Exception ex)
            {
                // Als er nog andere problemen zijn gaan we ook naar de error pagina
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }


        //dit is de effectieve edit methode
        //Hier worden de gegevens die we hebben ingevuld in de edit view naar de database gestuurd
        //Als je op de edit view op de edit knop drukt, dan wordt deze methode aangeroepen
        //Image is hier geen string maar een HttpPostedFileBase
        //Dit is een bestand dat we kunnen uploaden

        [HttpPost]
        public ActionResult Edit(int id, string Name, bool IsBoxed, HttpPostedFileBase Image, string Description,
            decimal Price, int Stock, Platform Platform, Genre Genre, string Developer, DateTime ReleaseDate, int AgeRating)
        {
            string ImageName;

            //We vragen de game op bij de bll
            Game existingGame = TimeWarpGames.Bll.GameBll.ReadOne(id);
            if (existingGame == null)
            {
                return View("Error"); //Als het spel niet gevonden wordt, gaan we naar de errorpagina
            }

            ImageName = existingGame.Image; // Als de image niet wordt vervangen, behouden we de oude naam

            // Hier controleren we of er een nieuwe afbeelding is geüpload
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

            //Hier geven we de aangepaste gegevens door aan de bll
            bool gameUpdated = TimeWarpGames.Bll.GameBll.Update(id, Name, IsBoxed, ImageName, Description, Price, Stock,
                Platform, Genre, Developer, ReleaseDate, AgeRating);

            //Hier controleren we of het spel is bijgewerkt
            //Als het spel is bijgewerkt, geven we een feedback weer
            if (gameUpdated)
            {
                ViewBag.Feedback = Name + " is bijgewerkt";
            }
            //Als het spel niet is bijgewerkt, geven we een feedback weer
            else
            {
                ViewBag.Feedback = "Bijwerken van spel is niet gelukt";
            }


            return RedirectToAction("Details", new{id = id}); // Re-fetch and display the updated game
        }




    }
}
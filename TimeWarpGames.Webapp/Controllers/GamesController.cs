using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Create(string Name, bool IsBoxed, string Image, string Description, decimal Price,
            int Stock,
            Platform Platform, Genre Genre, string Developer, DateTime ReleaseDate, int AgeRating)
        {
            bool memberCreated = TimeWarpGames.Bll.GameBll.Create(Name, IsBoxed, Image, Description, Price, Stock,
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
    }
}
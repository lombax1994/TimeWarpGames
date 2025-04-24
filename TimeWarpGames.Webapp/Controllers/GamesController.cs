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
    }
}
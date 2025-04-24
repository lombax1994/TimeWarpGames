using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeWarpGames.Webapp.Controllers
{
    public class ConsolesController : Controller
    {
        // GET: Consoles
        public ActionResult Index()
        {
            try
            {
                List<TimeWarpGames.Entities.Console> lstConsoles = TimeWarpGames.Bll.ConsoleBll.ReadAll();
                return View(lstConsoles);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                // Redirect to an error page or show an error message
                ViewBag.ErrorMessage = "Er werden geen consoles gevonden.";
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name, bool IsBoxed, string Image, string Description, decimal Price,
            int Stock, string Brand, string Model, DateTime ReleaseDate, TimeWarpGames.Entities.State State)
        {

            bool consoleCreated = TimeWarpGames.Bll.ConsoleBll.Create(Name, IsBoxed, Image, Description, Price, Stock,
                Brand, Model, ReleaseDate, State);
            if (consoleCreated)
            {
                ViewBag.Feedback = Name + " is aangemaakt.";
            }
            else
            {
                ViewBag.Feedback = "Console kon niet aangemaakt worden.";
            }

            return View();

        }
    }
}
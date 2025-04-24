using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeWarpGames.Bll;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Webapp.Controllers
{
    public class AccessoriesController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                List<Accessory> lstAccessories = AccessoryBll.ReadAll();
                return View(lstAccessories);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name, bool IsBoxed, string Image, string Description, decimal Price,
            int Stock, string Brand, Platform Platform, AccessoryType Type, State State)
        {
            bool accessoryCreated = AccessoryBll.Create(Name, IsBoxed, Image, Description, Price, Stock, Brand,
                Platform, Type, State);
            if (accessoryCreated)
            {
                ViewBag.Feedback = Name + " is toegevoegd";
            }
            else
            {
                ViewBag.Feedback = "Accessoire toevoegen is mislukt";
            }

            return View();
        }
    }
}
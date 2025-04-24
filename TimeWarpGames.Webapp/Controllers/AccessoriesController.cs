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
    }
}
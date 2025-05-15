using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Webapp.Controllers
{
    public class ProductsController : BaseController
    {
        // GET: Products
        public ActionResult Index()
        {
            try
            {
                List<Product> lstProducts = TimeWarpGames.Bll.ProductBll.ReadAll();
                return View(lstProducts);
            }
            catch (Exception ex)
            {
                // Log the exception
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
    }
}
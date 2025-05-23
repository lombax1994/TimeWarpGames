﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeWarpGames.Bll;
using Console = TimeWarpGames.Entities.Console;

namespace TimeWarpGames.Webapp.Controllers
{
    public class ConsolesController : BaseController
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

        [Authorize(Roles = "StoreManager")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name, bool IsBoxed, HttpPostedFileBase Image, string Description,
            decimal Price,
            int Stock, string Brand, string Model, DateTime ReleaseDate, TimeWarpGames.Entities.State State)
        {
            string ImageName = "~/Content/Images/placeholder.png";

            if (Image != null)
            {
                if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png")
                {
                    string PathToSave = Server.MapPath("~/Content/Images/ConsolePics/");
                    string imageExtension = Path.GetExtension(Image.FileName);
                    ImageName = Guid.NewGuid() + imageExtension;
                    PathToSave += ImageName;
                    Image.SaveAs(PathToSave);
                }
            }


            bool consoleCreated = TimeWarpGames.Bll.ConsoleBll.Create(Name, IsBoxed, ImageName, Description, Price,
                Stock,
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

        public ActionResult Details(int id)
        {
            try
            {
                var console = TimeWarpGames.Bll.ConsoleBll.ReadOne(id);

                var cartBll = new TimeWarpGames.Bll.ShoppingCartBll(Session);
                int quantityInCart = cartBll.GetQuantityInCart(console.ProductId);

                int availableStock = console.Stock - quantityInCart;
                if (availableStock < 0) availableStock = 0;

                ViewBag.ActueleVoorraad = availableStock;

                var reviews = ReviewBll.ReadByProductId(console.ProductId);
                ViewBag.Reviews = reviews;

                return View(console);
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
                Console console = TimeWarpGames.Bll.ConsoleBll.ReadOne(id);
                return View(console);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            int consoleId = Convert.ToInt32(id);
            bool consoleDeleted = TimeWarpGames.Bll.ConsoleBll.Delete(consoleId);
            if (consoleDeleted)
            {
                TempData["Feedback"] = "Console is verwijderd.";
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
                Console console = TimeWarpGames.Bll.ConsoleBll.ReadOne(id);
                if (console == null)
                {
                    return View("Error");
                }

                return View(console);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, string Name, bool IsBoxed, HttpPostedFileBase Image, string Description,
            decimal Price, int Stock, string Brand, string Model, DateTime ReleaseDate,
            TimeWarpGames.Entities.State State)
        {
            string ImageName;

            Console existingConsole = TimeWarpGames.Bll.ConsoleBll.ReadOne(id);
            if (existingConsole == null)
            {
                return View("Error");
            }

            ImageName = existingConsole.Image;

            if (Image != null)
            {
                if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png")
                {
                    string PathToSave = Server.MapPath("~/Content/Images/ConsolePics/");
                    string imageExtension = Path.GetExtension(Image.FileName);
                    ImageName = Guid.NewGuid() + imageExtension;
                    PathToSave += ImageName;
                    Image.SaveAs(PathToSave);
                }
            }

            bool consoleUpdated = TimeWarpGames.Bll.ConsoleBll.Update(id, Name, IsBoxed, ImageName, Description, Price,
                Stock, Brand, Model, ReleaseDate, State);

            if (consoleUpdated)
            {
                ViewBag.FeedBack = Name + "is bijgewerkt.";
            }
            else
            {
                ViewBag.FeedBack = "Console kon niet bijgewerkt worden.";
            }

            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddReview(int productId, string comment, int rating)
        {
            string userName = User.Identity.Name;

            bool success = ReviewBll.Create(userName, comment, rating, productId);

            if (!success)
            {
                TempData["Error"] = "Er is een fout opgetreden bij het toevoegen van je review.";
            }

            return RedirectToAction("Details", new { id = productId });
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TimeWarpGames.Bll;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Webapp.Controllers
{
    public class AccessoriesController : BaseController
    {
        // GET: Accessories
        public ActionResult Index()
        {
            try
            {
                List<Accessory> lstAccessories = AccessoryBll.ReadAll();
                return View(lstAccessories);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Er werden geen accessoires gevonden.";
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
            decimal Price, int Stock, string Brand, Platform Platform, AccessoryType Type, State State)
        {
            string ImageName = "~/Content/Images/placeholder.png";

            if (Image != null)
            {
                if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png")
                {
                    string pathToSave = Server.MapPath("~/Content/Images/AccessoryPics/");
                    string extension = Path.GetExtension(Image.FileName);
                    ImageName = Guid.NewGuid() + extension;
                    pathToSave += ImageName;
                    Image.SaveAs(pathToSave);
                }
            }

            bool accessoryCreated = AccessoryBll.Create(Name, IsBoxed, ImageName, Description, Price,
                Stock, Brand, Platform, Type, State);

            if (accessoryCreated)
            {
                ViewBag.Feedback = Name + " is toegevoegd.";
            }
            else
            {
                ViewBag.Feedback = "Accessoire kon niet toegevoegd worden.";
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            try
            {
                Accessory accessory = AccessoryBll.ReadOne(id);

                var cartBll = new ShoppingCartBll(Session);
                int quantityInCart = cartBll.GetQuantityInCart(accessory.ProductId);

                int availableStock = accessory.Stock - quantityInCart;
                if (availableStock < 0) availableStock = 0;

                ViewBag.ActueleVoorraad = availableStock;

                var reviews = ReviewBll.ReadByProductId(accessory.ProductId);
                ViewBag.Reviews = reviews;

                return View(accessory);
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
                Accessory accessory = AccessoryBll.ReadOne(id);
                return View(accessory);
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
            int accessoryId = Convert.ToInt32(id);
            bool accessoryDeleted = AccessoryBll.Delete(accessoryId);
            if (accessoryDeleted)
            {
                TempData["Feedback"] = "Accessoire is verwijderd.";
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
                Accessory accessory = AccessoryBll.ReadOne(id);
                if (accessory == null)
                {
                    return View("Error");
                }

                return View(accessory);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, string Name, bool IsBoxed, HttpPostedFileBase Image, string Description,
            decimal Price, int Stock, string Brand, Platform Platform, AccessoryType Type, State State)
        {
            string ImageName;

            Accessory existingAccessory = AccessoryBll.ReadOne(id);
            if (existingAccessory == null)
            {
                return View("Error");
            }

            ImageName = existingAccessory.Image;

            if (Image != null)
            {
                if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png")
                {
                    string pathToSave = Server.MapPath("~/Content/Images/AccessoryPics/");
                    string extension = Path.GetExtension(Image.FileName);
                    ImageName = Guid.NewGuid() + extension;
                    pathToSave += ImageName;
                    Image.SaveAs(pathToSave);
                }
            }

            bool accessoryUpdated = AccessoryBll.Update(id, Name, IsBoxed, ImageName, Description,
                Price, Stock, Brand, Platform, Type, State);

            if (accessoryUpdated)
            {
                TempData["Feedback"] = Name + " is bijgewerkt.";
            }
            else
            {
                TempData["Feedback"] = "Accessoire kon niet bijgewerkt worden.";
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

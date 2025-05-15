using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeWarpGames.Bll;
using TimeWarpGames.Entities;
using TimeWarpGames.Webapp.Models;

namespace TimeWarpGames.Webapp.Controllers
{
    public class AccessoriesController : BaseController
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

        [Authorize(Roles="StoreManager")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name, bool IsBoxed, HttpPostedFileBase Image, string Description, decimal Price,
            int Stock, string Brand, Platform Platform, AccessoryType Type, State State)
        {
            string ImageName = "placeholder.png";

            if (Image != null)
            {
                if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png")
                {
                    string pathToSave = Server.MapPath("~/Content/Images/AccessoryPics/");
                    string pictureExtension = Path.GetExtension(Image.FileName);
                    ImageName = Name + pictureExtension;
                    pathToSave += ImageName;
                    Image.SaveAs(pathToSave);
                }
            }

            bool accessoryCreated = AccessoryBll.Create(Name, IsBoxed, ImageName, Description, Price, Stock, Brand,
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

        public ActionResult Details(int id)
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
                    TempData["Feedback"] = "Accessoire is verwijderd";
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
                    string pictureExtension = Path.GetExtension(Image.FileName);
                    ImageName = Guid.NewGuid() + pictureExtension;
                    pathToSave += ImageName;
                    Image.SaveAs(pathToSave);
                }
            }

            bool accessoryUpdated = AccessoryBll.Update(id, Name, IsBoxed, ImageName, Description, Price, Stock, Brand,
                Platform, Type, State);
            if (accessoryUpdated)
            {
                TempData["Feedback"] = Name + " is aangepast";
               
            }
            else
            {
                TempData["Feedback"] = "Accessoire aanpassen is mislukt";
              
            }

            return RedirectToAction("Details", new { id = id });

        }
    }
}
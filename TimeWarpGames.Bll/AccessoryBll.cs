using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters;
using TimeWarpGames.Dal;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Bll
{
    public static class AccessoryBll
    {
        public static List<Accessory> ReadAll()
        {
            List<Accessory> lstAccessories = AccessoryDal.ReadAll();
            if (lstAccessories == null)
            {
                throw new Exception("Geen accessoires gevonden");
            }

            return lstAccessories;
        }

        public static bool Create(string Name, bool IsBoxed, string Image, string Description, decimal Price,
            int Stock, string Brand, Platform Platform, AccessoryType Type, State State)
        {
            Name = Name.Trim();
            Description = Description.Trim();
            Brand = Brand.Trim();

            Accessory accessory = new Accessory(Name, IsBoxed, Image, Description, Price, Stock, Brand, Platform,
                Type, State);
            bool accessoryCreated = AccessoryDal.Create(accessory);
            return accessoryCreated;
        }

        public static Accessory ReadOne(int id)
        {
            Accessory accessory = AccessoryDal.ReadOne(id);
            if (accessory == null)
            {
                throw new Exception("Geen accessoire gevonden");
            }
            return accessory;
        }

        public static bool Delete(int id)
        {
            try
            {
                Accessory accessory = AccessoryDal.ReadOne(id);
                bool accessoryDeleted = AccessoryDal.Delete(accessory);
                return accessoryDeleted;
            }
            catch
            {
                return false;
            }
        }

        public static bool Update(int accessoryId, string updatedName, bool updatedIsBoxed, string updatedImage,
            string updatedDescription, decimal updatedPrice, int updatedStock, string updatedBrand,
            Platform updatedPlatform, AccessoryType updatedType, State updatedState)
        {
            Accessory accessory = AccessoryDal.ReadOne(accessoryId);

            if (accessory == null)
            {
                return false;
            }

            //trim strings
            updatedName = updatedName.Trim();
            updatedDescription = updatedDescription.Trim();
            updatedBrand = updatedBrand.Trim();

            //controleer of getallen niet negatief zijn
            if (updatedPrice < 0 || updatedStock < 0)
            {
                return false;
            }
            //controleer op lege velden, anders return false
            if (string.IsNullOrEmpty(updatedName) || string.IsNullOrEmpty(updatedDescription) ||
                string.IsNullOrEmpty(updatedBrand))
            {
                throw new Exception("Vul alle velden in");
            }
            //update de velden
            accessory.Name = updatedName;
            accessory.IsBoxed = updatedIsBoxed;
            accessory.Image = updatedImage;
            accessory.Description = updatedDescription;
            accessory.Price = updatedPrice;
            accessory.Stock = updatedStock;
            accessory.Brand = updatedBrand;
            accessory.Platform = updatedPlatform;
            accessory.Type = updatedType;
            accessory.State = updatedState;

            bool accessoryUpdated = AccessoryDal.Update(accessory);
            return accessoryUpdated;


        }
    }
}
using System;
using System.Collections.Generic;
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
            Accessory accessory = new Accessory(Name, IsBoxed, Image, Description, Price, Stock, Brand, Platform,
                Type, State);
            bool accessoryCreated = AccessoryDal.Create(accessory);
            return accessoryCreated;
        }
    }
}
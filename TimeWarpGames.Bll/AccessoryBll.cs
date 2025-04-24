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
}
}
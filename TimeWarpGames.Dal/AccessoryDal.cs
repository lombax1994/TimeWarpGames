using System.Collections.Generic;
using System.Linq;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Dal
{
    public static class AccessoryDal
    {
        public static List<Accessory> ReadAll()
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                List<Accessory> lstAccessories = db.Accessories.ToList();
                return lstAccessories;
            }
        }
    }
}
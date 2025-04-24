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

        public static bool Create(Accessory accessory)
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                db.Accessories.Add(accessory);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
using System;
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
                try
                {
                    db.Accessories.Add(accessory);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        return true;
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            }

        }

        public static Accessory ReadOne(int id)
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                Accessory accessory = db.Accessories.Find(id);
                return accessory;
            }
        }

        public static bool Delete(Accessory accessory)
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                db.Entry(accessory).State = System.Data.Entity.EntityState.Deleted;
                int i = db.SaveChanges();
                if (i > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public static bool Update(Accessory updatedAccessory)
        {

            try
            {
                using (var db = new TimeWarpGamesDbContext())
                {
                    db.Entry(updatedAccessory).State = System.Data.Entity.EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
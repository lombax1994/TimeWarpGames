using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Dal
{
    public static class ConsoleDal
    {

        //ik lees alle consoles uit de database.
        public static List<Console> ReadAll()
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                List<Console> lstConsoles = db.Consoles.ToList();
                return lstConsoles;
            }
        }

        public static bool Create(TimeWarpGames.Entities.Console console)
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                try
                {

                    db.Consoles.Add(console);
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

        public static Console ReadOne(int id)
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                Console console = db.Consoles.Find(id);
                return console;
            }
        }

        public static bool Delete(Console console)
        {
            using(var db = new TimeWarpGamesDbContext())
            {
                db.Entry(console).State = EntityState.Deleted;
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
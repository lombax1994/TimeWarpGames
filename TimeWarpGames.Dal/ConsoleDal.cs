using System.Collections.Generic;
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
                db.Consoles.Add(console);
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
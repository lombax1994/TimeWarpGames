using System.Collections.Generic;
using System.Linq;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Dal
{
    public static class GameDal
    {
        public static List<Game> ReadAll()
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                List<Game> lstGames = db.Games.ToList();
                return lstGames;
            }
        }

        public static bool Create(Game game)
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                try
                {

                    db.Games.Add(game);
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
    }
}
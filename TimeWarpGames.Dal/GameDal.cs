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
    }
}
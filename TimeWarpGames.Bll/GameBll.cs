using System;
using System.Collections.Generic;
using TimeWarpGames.Dal;
using TimeWarpGames.Entities;

namespace TimeWarpGames.Bll
{
    public static class GameBll
    {
        public static List<Game> ReadAll()
        {
            List<Game> lstGames = GameDal.ReadAll();
            if (lstGames == null)
            {
                throw new Exception("Geen Games gevonden");
            }

            return lstGames;
        }

        public static bool Create(string Name, bool IsBoxed, string Image, string Description, decimal Price, int Stock,
            Platform Platform, Genre Genre, string Developer, DateTime ReleaseDate, int AgeRating)
        {
            Game game = new Game(Name, IsBoxed, Image, Description, Price, Stock, Platform, Genre, Developer,
                ReleaseDate, AgeRating);
            bool gameCreated = GameDal.Create(game);
            return gameCreated;
        }
    }
}
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

            Name = Name.Trim();
            Description = Description.Trim();
            Developer = Developer.Trim();

            Game game = new Game(Name, IsBoxed, Image, Description, Price, Stock, Platform, Genre, Developer,
                ReleaseDate, AgeRating);
            bool gameCreated = GameDal.Create(game);
            return gameCreated;
        }

        public static Game ReadOne(int id)
        {
            Game game = GameDal.ReadOne(id);
            if (game == null)
            {
                throw new Exception("Geen Game gevonden");
            }

            return game;
        }

        public static bool Delete(int id)
        {
            try
            {
                Game game = GameDal.ReadOne(id);
                bool gameDeleted = GameDal.Delete(game);
                return gameDeleted;
            }
            catch
            {
                return false;
            }
        }
    }
}
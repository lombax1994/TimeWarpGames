using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

        public static Game ReadOne(int id)
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                Game game = db.Games.Find(id);
                return game;
            }
        }

        public static bool Delete(Game game)
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                db.Entry(game).State = EntityState.Deleted;
                int i = db.SaveChanges();
                if (i > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public static bool Update(Game updatedGame)
        {
            using (var db = new TimeWarpGamesDbContext())
            {
                try
                {
                    // AddOrUpdate will add or update the entity depending on whether it already exists
                    db.Games.AddOrUpdate(updatedGame);

                    // Save the changes to the database
                    int numberOfChanges = db.SaveChanges();

                    // Return true if changes were made
                    return numberOfChanges > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


    }
}
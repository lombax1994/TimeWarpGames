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

        public static bool Update(int gameId, string updatedName, bool updatedIsBoxed, string updatedImage,
            string updatedDescription, decimal updatedPrice, int updatedStock, Platform updatedPlatform,
            Genre updatedGenre, string updatedDeveloper, DateTime updatedReleaseDate, int updatedAgeRating)
        {
            // Retrieve the game using its ID
            Game game = GameDal.ReadOne(gameId);

            if (game == null)
            {
                return false; // Return false if the game is not found
            }

            // Trim the input strings
            updatedName = updatedName.Trim();
            updatedDescription = updatedDescription.Trim();
            updatedDeveloper = updatedDeveloper.Trim();

            // Check if required fields are valid
            if (string.IsNullOrEmpty(updatedName) || string.IsNullOrEmpty(updatedDescription) || string.IsNullOrEmpty(updatedDeveloper))
            {
                return false; // Return false if any required field is missing
            }

            // Update the game's properties with the new values
            game.Name = updatedName;
            game.IsBoxed = updatedIsBoxed;
            game.Image = updatedImage; // Assuming the image can be updated (this could be handled separately for file uploads)
            game.Description = updatedDescription;
            game.Price = updatedPrice;
            game.Stock = updatedStock;
            game.Platform = updatedPlatform;
            game.Genre = updatedGenre;
            game.Developer = updatedDeveloper;
            game.ReleaseDate = updatedReleaseDate;
            game.AgeRating = updatedAgeRating;

            // Call the DAL method to update the game in the database
            bool gameUpdated = GameDal.Update(game);

            return gameUpdated; // Return whether the update was successful
        }
    }
}
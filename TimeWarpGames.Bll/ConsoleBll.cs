using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TimeWarpGames.Dal;
using Console = TimeWarpGames.Entities.Console;

namespace TimeWarpGames.Bll
{
    public static class ConsoleBll
    {
        //ik controleer of de uitgelezen consoles niet null zijn en of er consoles zijn gevonden.
        //als dat alles in orde is dan geef ik de lijst met consoles terug.
        public static List<TimeWarpGames.Entities.Console> ReadAll()
        {
            List<TimeWarpGames.Entities.Console> lstConsoles = ConsoleDal.ReadAll();
            if (lstConsoles == null || lstConsoles.Count == 0)
            {
                throw new Exception("No consoles found.");
            }

            return lstConsoles;
        }

        public static bool Create(string Name, bool IsBoxed, string Image, string Description, decimal Price,
            int Stock, string Brand, string Model, DateTime ReleaseDate, TimeWarpGames.Entities.State State)
        {
            Name = Name.Trim();
            Description = Description.Trim();
            Brand = Brand.Trim();
            Model = Model.Trim();
            Image = Image.Trim();


            TimeWarpGames.Entities.Console console = new TimeWarpGames.Entities.Console(Name, IsBoxed, Image,
                Description, Price, Stock, Brand, Model, ReleaseDate, State);
            bool memberCreated = ConsoleDal.Create(console);
            return memberCreated;
        }

        public static Console ReadOne(int id)
        {
            Console console = ConsoleDal.ReadOne(id);
            if (console == null)
            {
                throw new Exception("Console not found.");
            }

            return console;
        }

        public static bool Delete(int id)
        {
            try
            {
                Console console = ConsoleDal.ReadOne(id);
                bool consoleDeleted = ConsoleDal.Delete(console);
                return consoleDeleted;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool Update(int consoleId, string updatedName, bool updatedIsBoxed, string updatedImage,
            string updatedDescription, decimal updatedPrice, int updatedStock, string updatedBrand, string updatedModel,
            DateTime updatedReleaseDate, TimeWarpGames.Entities.State updatedState)
        {
            Console console = ConsoleDal.ReadOne(consoleId);
            if (console == null)
            {
                throw new Exception("Console not found.");
            }

            //trim de invoer
            updatedName = updatedName.Trim();
            updatedDescription = updatedDescription.Trim();
            updatedBrand = updatedBrand.Trim();
            updatedModel = updatedModel.Trim();

            if (string.IsNullOrEmpty(updatedName) ||
                string.IsNullOrEmpty(updatedBrand) ||
                string.IsNullOrEmpty(updatedDescription) ||
                string.IsNullOrEmpty(updatedModel) ||
                updatedStock < 0 ||
                updatedPrice <= 0)
            {
                return false;
            }

            //update de console
            console.Name = updatedName;
            console.IsBoxed = updatedIsBoxed;
            console.Image = updatedImage;
            console.Description = updatedDescription;
            console.Price = updatedPrice;
            console.Stock = updatedStock;
            console.Brand = updatedBrand;
            console.Model = updatedModel;
            console.ReleaseDate = updatedReleaseDate;
            console.State = updatedState;

            //update de console in de database
            bool consoleUpdated = ConsoleDal.Update(console);
            return consoleUpdated;

        }
    }
}
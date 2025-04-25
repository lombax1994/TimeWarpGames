using System;
using System.Collections.Generic;
using TimeWarpGames.Dal;

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
    }
}
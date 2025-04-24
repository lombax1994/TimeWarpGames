using TimeWarpGames.Entities;

namespace TimeWarpGames.Dal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TimeWarpGames.Dal.TimeWarpGamesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TimeWarpGames.Dal.TimeWarpGamesDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Games.AddOrUpdate(
                g => g.Name,
                new Game(
                    name: "The Legend of Zelda: Breath of the Wild",
                    isBoxed: true,
                    image: "Content/images/zelda.jpg",
                    description: "Een episch open wereld avontuur met Link.",
                    price: 59.99m,
                    stock: 100,
                    platform: Platform.NES,
                    genre: Genre.Adventure,
                    developer: "Nintendo",
                    releaseDate: new DateTime(2017, 3, 3),
                    ageRating: 9
                )
            );

            context.Consoles.AddOrUpdate(
                c => c.Name,
                //Hier moest ik duidelijk maken dat ik een Console object aan het maken was
                //want console is ook een ingebouwde class in C#
                new TimeWarpGames.Entities.Console(
                    name: "Nintendo Entertainment System (NES)",
                    isBoxed: true,
                    image: "Content/images/nes.jpg",
                    description: "De originele 8-bit console van Nintendo uit de jaren 80. Retro gaming op z'n best!",
                    price: 89.99m,
                    stock: 25,
                    brand: "Nintendo",
                    model: "NES-001",
                    releaseDate: new DateTime(1985, 10, 18),
                    state: State.Refurbished
                )
            );

            context.Accessories.AddOrUpdate(
                a => a.Name,
                new Accessory(
                    name: "Nintendo 64 Controller (Original Grey)",
                    isBoxed: true,
                    image: "Content/images/n64-controller.jpg",
                    description: "Originele N64 controller in klassieke grijze uitvoering. Voor de ultieme retro ervaring.",
                    price: 29.99m,
                    stock: 40,
                    brand: "Nintendo",
                    platform: Platform.N64,
                    type: AccessoryType.Controller,
                    state: State.Gebruikt
                )
            );



        }
    }
}

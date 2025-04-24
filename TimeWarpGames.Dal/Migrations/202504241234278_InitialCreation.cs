namespace TimeWarpGames.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        IsBoxed = c.Boolean(nullable: false),
                        Image = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 500),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        Brand = c.String(maxLength: 100),
                        Platform = c.Int(),
                        Type = c.Int(),
                        State = c.Int(),
                        Brand1 = c.String(),
                        Model = c.String(),
                        ReleaseDate = c.DateTime(),
                        State1 = c.Int(),
                        Platform1 = c.Int(),
                        Genre = c.Int(),
                        Developer = c.String(maxLength: 100),
                        ReleaseDate1 = c.DateTime(),
                        AgeRating = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}

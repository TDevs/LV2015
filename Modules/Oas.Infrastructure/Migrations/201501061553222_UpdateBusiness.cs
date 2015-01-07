namespace Oas.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBusiness : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Businesses", "IsAlcohol");
            DropColumn("dbo.Businesses", "IsAlcohol_bar");
            DropColumn("dbo.Businesses", "IsAlcohol_beer_wine");
            DropColumn("dbo.Businesses", "Attire");
            DropColumn("dbo.Businesses", "Country");
            DropColumn("dbo.Businesses", "Locality");
            DropColumn("dbo.Businesses", "Meal_breakfast");
            DropColumn("dbo.Businesses", "Meal_cater");
            DropColumn("dbo.Businesses", "Meal_deliver");
            DropColumn("dbo.Businesses", "Meal_takeout");
            DropColumn("dbo.Businesses", "Open_24hrs");
            DropColumn("dbo.Businesses", "Options_healthy");
            DropColumn("dbo.Businesses", "Options_vegetarian");
            DropColumn("dbo.Businesses", "Payment_cashonly");
            DropColumn("dbo.Businesses", "Postcode");
            DropColumn("dbo.Businesses", "Price");
            DropColumn("dbo.Businesses", "Rating");
            DropColumn("dbo.Businesses", "Region");
            DropColumn("dbo.Businesses", "Wifi");
            DropColumn("dbo.Businesses", "IsClosed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Businesses", "IsClosed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "Wifi", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "Region", c => c.String());
            AddColumn("dbo.Businesses", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.Businesses", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Businesses", "Postcode", c => c.String());
            AddColumn("dbo.Businesses", "Payment_cashonly", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "Options_vegetarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "Options_healthy", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "Open_24hrs", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "Meal_takeout", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "Meal_deliver", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "Meal_cater", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "Meal_breakfast", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "Locality", c => c.String());
            AddColumn("dbo.Businesses", "Country", c => c.String());
            AddColumn("dbo.Businesses", "Attire", c => c.String());
            AddColumn("dbo.Businesses", "IsAlcohol_beer_wine", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "IsAlcohol_bar", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "IsAlcohol", c => c.Boolean(nullable: false));
        }
    }
}

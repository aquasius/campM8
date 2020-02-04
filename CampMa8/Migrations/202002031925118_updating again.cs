namespace CampMa8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingagain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Campgrounds", "amenities", c => c.String());
            AddColumn("dbo.Campgrounds", "hookups", c => c.String());
            DropColumn("dbo.Campgrounds", "sitesWithAmps");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Campgrounds", "sitesWithAmps", c => c.String());
            DropColumn("dbo.Campgrounds", "hookups");
            DropColumn("dbo.Campgrounds", "amenities");
        }
    }
}

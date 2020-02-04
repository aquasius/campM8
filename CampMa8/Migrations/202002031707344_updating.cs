namespace CampMa8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campgrounds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        contractID = c.String(nullable: false),
                        contractType = c.String(),
                        facilityID = c.String(),
                        facilityName = c.String(),
                        faciltyPhoto = c.String(),
                        siteType = c.String(),
                        sitesWithPetsAllowed = c.String(),
                        sitesWithWaterfront = c.String(),
                        sitesWithAmps = c.String(),
                        Latitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Campgrounds");
        }
    }
}

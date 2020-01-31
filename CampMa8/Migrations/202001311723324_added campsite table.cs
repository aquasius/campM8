namespace CampMa8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcampsitetable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Campers", "CampExperience", c => c.Double(nullable: false));
            AlterColumn("dbo.CampExperiences", "Experience", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CampExperiences", "Experience", c => c.Int(nullable: false));
            AlterColumn("dbo.Campers", "CampExperience", c => c.Int(nullable: false));
        }
    }
}

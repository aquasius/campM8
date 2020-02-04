namespace CampMa8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcontracttypeandid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Campers", "ZipCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Campers", "ZipCode", c => c.Int(nullable: false));
        }
    }
}

namespace Music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artistNameRestrictions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artists", "Name", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artists", "Name", c => c.String());
        }
    }
}

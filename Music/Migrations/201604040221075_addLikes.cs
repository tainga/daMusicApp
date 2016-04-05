namespace Music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLikes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "likes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "likes");
        }
    }
}

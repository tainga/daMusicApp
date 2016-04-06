namespace Music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playlists2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        PlaylistID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PlaylistID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Playlists");
        }
    }
}

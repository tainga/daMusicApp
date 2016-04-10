namespace Music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IEnumerable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlaylistAlbums", "Playlist_PlaylistID", "dbo.Playlists");
            DropForeignKey("dbo.PlaylistAlbums", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.PlaylistAlbums", new[] { "Playlist_PlaylistID" });
            DropIndex("dbo.PlaylistAlbums", new[] { "Album_AlbumID" });
            DropTable("dbo.PlaylistAlbums");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlaylistAlbums",
                c => new
                    {
                        Playlist_PlaylistID = c.Int(nullable: false),
                        Album_AlbumID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Playlist_PlaylistID, t.Album_AlbumID });
            
            CreateIndex("dbo.PlaylistAlbums", "Album_AlbumID");
            CreateIndex("dbo.PlaylistAlbums", "Playlist_PlaylistID");
            AddForeignKey("dbo.PlaylistAlbums", "Album_AlbumID", "dbo.Albums", "AlbumID", cascadeDelete: true);
            AddForeignKey("dbo.PlaylistAlbums", "Playlist_PlaylistID", "dbo.Playlists", "PlaylistID", cascadeDelete: true);
        }
    }
}

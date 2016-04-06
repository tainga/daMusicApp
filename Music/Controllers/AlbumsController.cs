using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Music.Models;

namespace Music.Controllers
{
    public class AlbumsController : Controller
    {
        private MusicContext db = new MusicContext();

        // GET: Albums
        public ActionResult Index(string searchString)
        {
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);

            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(s => s.Title.Contains(searchString) || s.Genre.Name.Contains(searchString) || s.Artist.Name.Contains(searchString));
            }

            return View(albums.ToList());
        }

        public ActionResult Like(int? id)
        {
            //var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);
            //return View("Index", albums);
            Album album = db.Albums.Find(id);
            album.Likes++;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        public ActionResult ByGenre(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var albums = db.Albums.Include(a => a.Genre).Where(a => a.GenreID == id);
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.GenreID == id);

            if (albums.Count() == 0)
            {
                return View(albums);
            }
            return View("Index", albums);
        }

        public ActionResult ByArtist(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.ArtistID == id);
            if (albums.Count() == 0)
            {
                return View(albums);
            }
            return View("Index", albums);
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.AlbumID == id).SingleOrDefault();
            if (album == null)
            {
                return HttpNotFound();
            }
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.GenreID == album.GenreID || a.ArtistID == album.ArtistID).OrderByDescending(x => x.Likes).ToList();
            albums.Remove(db.Albums.Find(id));
            albums = albums.Take(5).ToList();
            ViewBag.Suggested = albums;
            
            return View(album);
        }


        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.ArtistID = new SelectList(db.Artists.OrderBy(x => x.Name), "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(db.Genres.OrderBy(x => x.Name), "GenreID", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumID,Title,GenreID,Price,ArtistID")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumID,Title,GenreID,Price,ArtistID")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.AlbumID == id).SingleOrDefault();
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.Artist = album.Artist;
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

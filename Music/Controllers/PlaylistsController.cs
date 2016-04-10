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
    public class PlaylistsController : Controller
    {
        private MusicContext db = new MusicContext();

        // GET: Playlists
        public ActionResult Index()
        {
            return View(db.Playlists.ToList());
        }

        public ActionResult AddAlbum(int? id)
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
            ViewBag.id = id;
            ViewBag.PlaylistID = new SelectList(db.Playlists.OrderBy(x => x.Name), "PlaylistID", "Name");
            return View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAlbum(FormCollection form)
        {
            //System.Diagnostics.Debug.WriteLine("album id: " + Url.RequestContext.RouteData.Values["id"]);
            int alid = Int32.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            if (form == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Include(x => x.Artist).Include(x => x.Genre).Include(x => x.Playlists).Where(x => x.AlbumID == alid).Single();
            int plid = Int32.Parse(form["PlaylistID"]);
            //System.Diagnostics.Debug.WriteLine("playlist id: " + test);
            Playlist playlist = db.Playlists.Include(x => x.Albums).Where(x => x.PlaylistID == plid).Single();
            if (ModelState.IsValid)
            {
                db.Playlists.Find(plid).Albums.Add(album);
                db.Albums.Find(alid).Playlists.Add(playlist);
                db.SaveChanges();
            }
            //ViewBag.test = playlist.Albums;
            return View("Details", playlist);
        }

        // GET: Playlists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            
            if (playlist == null)
            {
                return HttpNotFound();
            }
            //var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.Playlists.Contains(playlist));
            //ViewBag.al = albums;
            return View(playlist);
        }

        // GET: Playlists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaylistID,Name")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                if (!db.Playlists.Any(ac => ac.Name.Equals(playlist.Name)))
                {
                    db.Playlists.Add(playlist);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaylistID,Name")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Playlist playlist = db.Playlists.Find(id);
            db.Playlists.Remove(playlist);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MvcMusicStore.Models;

namespace MvcMusicStore.API.Controllers
{
    public class AlbumsController : ApiController
    {
        private MusicStoreEntities db = new MusicStoreEntities();

        // GET: api/Albums
        public IQueryable<Album> GetAlbums()
        {
            return db.Albums;
        }

        // GET: api/Albums/5
        [ResponseType(typeof(Album))]
        public async Task<IHttpActionResult> GetAlbum(int id)
        {
            Album album = await db.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlbumExists(int id)
        {
            return db.Albums.Count(e => e.AlbumId == id) > 0;
        }
    }
}
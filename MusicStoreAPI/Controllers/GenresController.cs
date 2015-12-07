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
    public class GenresController : ApiController
    {
        private MusicStoreEntities db = new MusicStoreEntities();

        // GET: api/Genres
        public IQueryable<Genre> GetGenres()
        {
            return db.Genres;
        }

        // GET: api/Genres/5
        [ResponseType(typeof(Genre))]
        public async Task<IHttpActionResult> GetGenre(int id)
        {
            Genre genre = await db.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }
              
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenreExists(int id)
        {
            return db.Genres.Count(e => e.GenreId == id) > 0;
        }
    }
}
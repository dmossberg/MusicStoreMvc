using MvcMusicStore.Models;
using MvcMusicStore.ServiceProxy;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        MusicStoreEntities storeDB = new MusicStoreEntities();
        MusicStoreEntities storeDB2 = new MusicStoreEntities();
        
        //
        // GET: /Store/

        public ActionResult Index()
        {
            var genres = storeDB.Genres.ToList();

            return View(genres);
        }

        //
        // GET: /Store/Browse?genre=Disco

        public ActionResult Browse(string genre)
        {
            // Retrieve Genre and its Associated Albums from database
            var genreModel = storeDB.Genres.Include("Albums")
                .Single(g => g.Name == genre);

            foreach (Album alb in genreModel.Albums)
            {
                string sql = "SELECT Name FROM Artists WHERE ArtistId = " + alb.ArtistId;
                var objCtx = ((IObjectContextAdapter)storeDB2).ObjectContext;
                var results = objCtx.ExecuteStoreQuery<string>(sql);
                objCtx.Connection.Close();
            }

            return View(genreModel);
        }

        //
        // GET: /Store/Details/5

        public async Task<ActionResult> Details(int id)
        {
            await MusicStoreAPIClient.retrieveFromCache();
            var album = storeDB.Albums.Find(id);
            //AuditEvent.Log.DetailsCalled(id);     
           
            return View(album);
        }

        //
        // GET: /Store/GenreMenu

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = storeDB.Genres.ToList();

            return PartialView(genres);
        }

    }
}
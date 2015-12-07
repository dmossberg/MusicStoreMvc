using MvcMusicStore.Models;
using StackExchange.Profiling;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        MusicStoreEntities storeDB = new MusicStoreEntities();

        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);
            //string value = BackEndProxy.getValue().Result;
            //AuditEvent.Log.HomeCalled(value);            

            return View(albums);
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            var profiler = MiniProfiler.Current; // it's ok if this is null

            using (profiler.Step("Doing complex stuff"))
            {
                return storeDB.Albums
                    .OrderByDescending(a => a.OrderDetails.Count())
                    .Take(count)
                    .ToList();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcMusicStore.API;
using System.Web.Http.Description;

namespace MvcMusicStore.API.Controllers
{
    public class CacheController : ApiController
    {
        // GET: api/Cache
        public IHttpActionResult GetCache()
        {
            BusinessLayer bl = new BusinessLayer(); 
            string resultsXml = bl.GetSomeResults(10000);
            return Ok(resultsXml);
        }
        
        // GET: api/Cache/5
        public IHttpActionResult GetCache(int id)
        {
            BusinessLayer bl = new BusinessLayer();
            string resultsXml = bl.GetSomeResults(id);
            return Ok(resultsXml);
        }
    }
}

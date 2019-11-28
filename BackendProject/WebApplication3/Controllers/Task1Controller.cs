using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    [Route("api/Task1")]
    public class Task1Controller : ApiController
    {
        
        [HttpGet]
        public IEnumerable<Models.SQLTask1_Result> Get(
            string status = null
            )
        {
            using (var context = new Models.SQLTask1Context())
            {
                return context.SQLTask1(status).ToList();
            }
        }
    }
}

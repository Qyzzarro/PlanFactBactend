using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    [Route("api/Task2")]
    public class Task2Controller : ApiController
    {
        [HttpGet]
        public IEnumerable<Models.SQLTask2_Result> Get(
            string status = null
            )
        {
            using (var context = new Models.SQLTask2Context())
            {
                return context.SQLTask2(status).ToList();
            }
        }
    }
}

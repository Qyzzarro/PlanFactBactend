using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    [Route("api/Client")]
    public class ClientController : ApiController
    {
        [HttpGet]
        public IEnumerable<Models.Client> Get(
            int? start_with_ClientId = null,
            int? end_with_ClientId = null,
            int? ClientId = null,
            string FName = null,
            string LName = null,
            DateTime? start_with_BirthDay = null,
            DateTime? end_with_BirthDay = null,
            DateTime? BirthDay = null,
            int offset = 0,
            int limit = 10,
            string sort_by = null,
            string order_by = null
            )
        {
            using (var context = new Models.ClientContext())
            {
                var clients =
                    from c in context.Clients

                    where (start_with_ClientId != null ? c.ID >= start_with_ClientId : true)
                    where (end_with_ClientId != null ? c.ID <= end_with_ClientId : true)
                    where (ClientId != null ? c.ID == ClientId : true)

                    where (FName != null ? c.FName.Contains(FName) : true)
                    where (LName != null ? c.LName.Contains(LName) : true)

                    where (start_with_BirthDay != null ? c.BirthDay >= start_with_BirthDay : true)
                    where (end_with_BirthDay != null ? c.BirthDay <= end_with_BirthDay : true)
                    where (BirthDay != null ? c.BirthDay == BirthDay : true)

                    select c;

                switch (sort_by)
                {
                    case "id":
                        if (order_by == "asc") clients = clients.OrderBy(clinet => clinet.ID);
                        else clients = clients.OrderByDescending(clinet => clinet.ID);
                        break;
                    case "fname":
                        if (order_by == "asc") clients = clients.OrderBy(clinet => clinet.FName);
                        else clients = clients.OrderByDescending(clinet => clinet.FName);
                        break;
                    case "lname":
                        if (order_by == "asc") clients = clients.OrderBy(clinet => clinet.LName);
                        else clients = clients.OrderByDescending(clinet => clinet.LName);
                        break;
                    case "birthday":
                        if (order_by == "asc") clients = clients.OrderBy(clinet => clinet.BirthDay);
                        else clients = clients.OrderByDescending(clinet => clinet.BirthDay);
                        break;
                    default:
                        if (order_by == "asc") clients = clients.OrderBy(clinet => clinet.ID);
                        else clients = clients.OrderByDescending(clinet => clinet.ID);
                        break;
                }

                return clients.Skip(offset).Take(limit).ToList();
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(
            Models.Client NewClient
            )
        {
            if (NewClient == null)
                return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);

            using (var context = new Models.ClientContext())
            {
                context.Clients.Add(NewClient);
                if (context.SaveChanges() == 1)
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK);
                else
                    return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }



        [HttpPut]
        public HttpResponseMessage Put(
            int ClientID,
            Models.Client ChangedClient
            )
        {
            using (var context = new Models.ClientContext())
            {
                var pool = (context.Clients.Find(ClientID) as Models.Client);

                if (pool == null)
                    return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);

                pool.FName = ChangedClient.FName;
                pool.LName = ChangedClient.LName;
                pool.BirthDay = ChangedClient.BirthDay;

                context.Entry(pool).State = System.Data.Entity.EntityState.Modified;

                if (context.SaveChanges() == 1)
                    return Request.CreateResponse(System.Net.HttpStatusCode.Created);
                else
                    return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }



        [HttpDelete]
        public HttpResponseMessage Delete(
            int ClientToBeDeleted
            )
        {
            using (var context = new Models.ClientContext())
            {
                var pool = (context.Clients.Find(ClientToBeDeleted) as Models.Client);

                if (pool == null)
                    return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);

                context.Clients.Remove(pool);

                if (context.SaveChanges() == 1)
                    return Request.CreateResponse(System.Net.HttpStatusCode.Created);
                else
                    return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
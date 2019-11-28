using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    [Route("api/Order")]
    public class OrderController : ApiController
    {
        [HttpGet]
        public IEnumerable<Models.Order> Get(
            int? start_with_orderid = null,
            int? end_with_orderid = null,
            int? OrderId = null,

            int? start_with_price = null,
            int? end_with_price = null,
            int? price = null,

            DateTime? start_with_Date_and_Time = null,
            DateTime? end_with_Date_and_Time = null,
            DateTime? Date_and_Time = null,

            int? start_with_client_id = null,
            int? end_with_client_id = null,
            int? ClientId = null,

            string status = null,

            int offset = 0,
            int limit = 10,
            string sort_by = null,
            string order_by = null
            )
        {
            using (var context = new Models.OrderContext())
            {
                var orders =
                    from o in context.Orders

                    where (start_with_orderid != null ? o.ID >= start_with_orderid : true)
                    where (end_with_orderid != null ? o.ID <= end_with_orderid : true)
                    where (OrderId != null ? o.ID == OrderId : true)

                    where (start_with_price != null ? o.ID >= start_with_price : true)
                    where (end_with_price != null ? o.ID <= end_with_price : true)
                    where (price != null ? o.ID == price : true)

                    where (status != null ? o.Status.Contains(status) : true)

                    where (start_with_Date_and_Time != null ? o.Date_and_Time >= start_with_Date_and_Time : true)
                    where (end_with_Date_and_Time != null ? o.Date_and_Time <= end_with_Date_and_Time : true)
                    where (Date_and_Time != null ? o.Date_and_Time == Date_and_Time : true)

                    where (start_with_client_id != null ? o.ClientID >= start_with_client_id : true)
                    where (end_with_client_id != null ? o.ClientID <= end_with_client_id : true)
                    where (ClientId != null ? o.ClientID == ClientId : true)

                    select o;

                switch (sort_by)
                {
                    case "id":
                        if (order_by == "asc") orders = orders.OrderBy(clinet => clinet.ID);
                        else orders = orders.OrderByDescending(clinet => clinet.ID);
                        break;
                    case "price":
                        if (order_by == "asc") orders = orders.OrderBy(order => order.Price);
                        else orders = orders.OrderByDescending(order => order.Price);
                        break;
                    case "date_and_time":
                        if (order_by == "asc") orders = orders.OrderBy(order => order.Date_and_Time);
                        else orders = orders.OrderByDescending(order => order.Date_and_Time);
                        break;
                    case "client_id":
                        if (order_by == "asc") orders = orders.OrderBy(order => order.ClientID);
                        else orders = orders.OrderByDescending(order => order.ClientID);
                        break;
                    default:
                        if (order_by == "asc") orders = orders.OrderBy(order => order.ID);
                        else orders = orders.OrderByDescending(order => order.ID);
                        break;
                }

                return orders.Skip(offset).Take(limit).ToList();
            }
        }



        [HttpPost]
        public HttpResponseMessage Post(
            Models.Order order
            )
        {
            if (order == null)
                return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);

            using (var context = new Models.OrderContext())
            {
                context.Orders.Add(order);
                if (context.SaveChanges() == 1)
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK);
                else
                    return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }



        [HttpPut]
        public HttpResponseMessage Put(
            int OrderId,
            Models.Order ChangedOrder
            )
        {
            using (var context = new Models.OrderContext())
            {
                var remoteOrder = (context.Orders.Find(OrderId) as Models.Order);

                if (remoteOrder == null)
                    return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);

                remoteOrder.Price = ChangedOrder.Price;
                remoteOrder.Date_and_Time = ChangedOrder.Date_and_Time;
                remoteOrder.Status = ChangedOrder.Status;
                remoteOrder.ClientID = ChangedOrder.ClientID;

                context.Entry(remoteOrder).State = System.Data.Entity.EntityState.Modified;

                if (context.SaveChanges() == 1)
                    return Request.CreateResponse(System.Net.HttpStatusCode.Created);
                else
                    return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }



        [HttpDelete]
        public HttpResponseMessage Delete(
            int OrderToBeDeleted
            )
        {
            using (var context = new Models.OrderContext())
            {
                var remoteOrder = (context.Orders.Find(OrderToBeDeleted) as Models.Order);

                if (remoteOrder == null)
                    return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);

                context.Orders.Remove(remoteOrder);

                if (context.SaveChanges() == 1)
                    return Request.CreateResponse(System.Net.HttpStatusCode.Created);
                else
                    return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}

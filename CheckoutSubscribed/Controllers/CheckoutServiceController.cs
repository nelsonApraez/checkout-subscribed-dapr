using Microsoft.AspNetCore.Mvc;
using Dapr;
using Dapr.Client;
using System.Text.Json.Serialization;

namespace CheckoutSubscribed.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutServiceController : Controller
    {
        public class Order
        {
            public string Environment
            {
                get;
                set;
            }

            public int Quantity
            {
                get;
                set;
            }

            public string Priority
            {
                get;
                set;
            }
        }
        

        //Subscribe to a topic 
        [Topic("pubsub", "topicCompany")]
        [HttpPost("/checkout")]
        public void Checkout([FromBody] Order order)
        {
            Console.WriteLine("Subscriber received : " + order.Environment + " " + order.Priority);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostApi.Controllers
{
    public class HomeController : ApiController
    {
        // GET api/demo 
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "World" };
        }
    }
}

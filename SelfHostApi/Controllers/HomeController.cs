using SelfHostApi.Abstract;
using SelfHostApi.Models;
using SelfHostApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostApi.Controllers
{
    public class HomeController : ApiController
    {
        private IRepository repo = null;
        public HomeController(IRepository repository)
        {
            repo = repository;
        }

        private class MessageView
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime Created { get; set; }
        }

        // GET api/home 
        [Authorize]
        public HttpResponseMessage Get(string name)
        {
            var query = repo.CreateQuery();
            var viewlist = new List<MessageView>();

            if(name!=null)
                query = 

            foreach (var item in query)
            {
                var view = new MessageView
                {
                    Name = item.Name,
                    Description = item.Description,
                    Created = item.Created
                };
                viewlist.Add(view);
            }
            return Request.CreateResponse(HttpStatusCode.OK, viewlist); 
        }


        [HttpPost]
        [Authorize]
        public HttpResponseMessage Create(string name, string description)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            var principal = (ClaimsPrincipal)Request.GetRequestContext().Principal;
            string roleValue = principal.Claims.FirstOrDefault(c => c.Type == "role").Value;
            string email = ClaimsPrincipal.Current.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            
            try
            {
                var userId = repo.GetUser(email)?.Id;

                var message = new Message
                {
                    ApplicationUserId = userId,
                    Name = name,
                    Description = description,
                    Created = DateTime.Now
                };
                repo.AddMessage(message);
                return Request.CreateResponse(HttpStatusCode.OK, "Message has been added.");
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

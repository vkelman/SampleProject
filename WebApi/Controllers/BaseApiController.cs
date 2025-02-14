using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        public HttpResponseMessage Found(object obj)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        public HttpResponseMessage Found()
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage DoesNotExist(string name)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound, $"{name} does not exist.");
        }

        public HttpResponseMessage AlreadyExist(string name)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, $"{name} already exists.");
        }
    }
}
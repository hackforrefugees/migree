using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("message")]
    public class MessageController : MigreeApiController
    {        

        [HttpGet, Route("{messageId:guid}")]
        public HttpResponseMessage GetMessageThread(Guid messageId)
        {
            try
            {
                

                return CreateApiResponse(HttpStatusCode.NoContent);
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
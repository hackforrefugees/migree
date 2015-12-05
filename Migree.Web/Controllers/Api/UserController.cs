using Migree.Core.Interfaces;
using Migree.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Web.Controllers.Api
{
    [RoutePrefix("~/api/user")]
    public class UserController : MigreeApiController
    {
        private IUserServant UserServant { get; }

        public UserController(IUserServant userServant)
        {
            UserServant = userServant;
        }

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(LoginRequest request)
        {
            if (UserServant.ValidateUser(request.Email, request.Password))
            {
                return CreateApiResponse(HttpStatusCode.OK);
            }

            return CreateApiResponse(HttpStatusCode.Unauthorized);
        }
    }
}

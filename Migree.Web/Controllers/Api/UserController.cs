using Migree.Core.Interfaces;
using Migree.Web.Models.Requests;
using Migree.Web.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Web.Controllers.Api
{
    [RoutePrefix("api/user")]
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
        [HttpPost]
        [Authorize]
        [Route("register")]
        public HttpResponseMessage Register(RegisterRequest request)
        {
            var user = UserServant.Register(request.Email, request.Password, request.FirstName, request.LastName, request.UserType);
            return CreateApiResponse(HttpStatusCode.OK, new RegisterResponse { UserId = user.Id });
        }
    }
}

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
    [RoutePrefix("api/competence")]
    public class CompetenceController : MigreeApiController
    {
        private ICompetenceServant CompetenceServant { get; }

        public CompetenceController(ICompetenceServant competenceServant)
        {
            CompetenceServant = competenceServant;
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public HttpResponseMessage GetCompetences()
        {
            var competences = CompetenceServant.GetCompetences();
            var response = competences.Select(x => new IdAndNameResponse { Id = x.Id, Name = x.Name }).ToList();
            return CreateApiResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage AddCompetences(AddCompetenceRequest request)
        {
            CompetenceServant.AddCompetence(request.Name);
            return CreateApiResponse(HttpStatusCode.NoContent);
        }
    }
}

using Migree.Core.Interfaces;
using Migree.Api.Models.Requests;
using Migree.Api.Models.Responses;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Api.Controllers.Api
{
    [RoutePrefix("competence")]
    public class CompetenceController : MigreeApiController
    {
        private ICompetenceServant CompetenceServant { get; }

        public CompetenceController(ICompetenceServant competenceServant)
        {
            CompetenceServant = competenceServant;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetCompetences()
        {
            try
            {
                var competences = CompetenceServant.GetCompetences();
                var response = competences.Select(x => new IdAndNameResponse { Id = x.Id, Name = x.Name }).ToList();
                return CreateApiResponse(HttpStatusCode.OK, response);
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage AddCompetence(AddCompetenceRequest request)
        {
            try
            {
                CompetenceServant.AddCompetence(request.Name);
                return CreateApiResponse(HttpStatusCode.NoContent);
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}

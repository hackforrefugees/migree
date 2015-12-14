using Migree.Api.Models.Requests;
using Migree.Api.Models.Responses;
using Migree.Core.Interfaces;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("competence")]
    public class CompetenceController : MigreeApiController
    {
        private ICompetenceServant CompetenceServant { get; }

        public CompetenceController(ICompetenceServant competenceServant)
        {
            CompetenceServant = competenceServant;
        }

        [HttpGet, Route(""), AllowAnonymous]
        public HttpResponseMessage GetCompetences()
        {
            var competences = CompetenceServant.GetCompetences();
            var response = competences.Select(x => new GuidIdAndNameResponse { Id = x.Id, Name = x.Name }).ToList();
            return CreateApiResponse(HttpStatusCode.OK, response);
        }

        [HttpPost, Route("")]
        public HttpResponseMessage AddCompetence(AddCompetencesRequest request)
        {
            foreach (var competence in request.Competences)
            {
                if (!string.IsNullOrWhiteSpace(competence))
                {
                    CompetenceServant.AddCompetence(competence);
                }
            }

            return CreateApiResponse(HttpStatusCode.NoContent);
        }
    }
}

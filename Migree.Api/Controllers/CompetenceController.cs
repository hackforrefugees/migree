using Migree.Api.Models;
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
        private IBusinessServant BusinessServant { get; }

        public CompetenceController(ICompetenceServant competenceServant, IBusinessServant businessServant)
        {
            CompetenceServant = competenceServant;
            BusinessServant = businessServant;
        }

        [HttpGet, Route(""), AllowAnonymous]
        public HttpResponseMessage GetCompetences()
        {
            var response = new GroupedCompetencesResponse();
            var businesses = BusinessServant.GetAll();
            
            foreach (var business in businesses)
            {
                var competences = CompetenceServant.GetCompetences(business.Type);

                if (competences.Count == 0)
                {
                    continue;
                }

                response.Add(new BusinessCompetenceResponse
                {
                    Business = new IntIdAndName { Id = business.Id, Name = business.Name },
                    Competences = competences.Select(x => new GuidIdAndName { Id = x.Id, Name = x.Name }).ToList()
                });
            }

            return CreateApiResponse(HttpStatusCode.OK, response);
        }

        [HttpPost, Route("")]
        public HttpResponseMessage AddCompetence(AddCompetencesRequest request)
        {
            //adding competences is an admin thing
            if (!Request.IsLocal())
            {
                return CreateApiResponse(HttpStatusCode.Unauthorized);
            }

            foreach (var competence in request.Competences)
            {
                if (!string.IsNullOrWhiteSpace(competence))
                {
                    CompetenceServant.AddCompetence(request.BusinessGroup, competence);
                }
            }

            return CreateApiResponse(HttpStatusCode.NoContent);
        }
    }
}

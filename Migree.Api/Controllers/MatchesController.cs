using Migree.Api.Models.Responses;
using Migree.Core.Interfaces;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("matches")]
    public class MatchesController : MigreeApiController
    {
        private const int NUMBER_OF_MATCHES_TO_TAKE = 50;

        private ICompetenceServant CompetenceServant { get; }
        private IUserServant UserServant { get; }        

        public MatchesController(ICompetenceServant compentenceServant, IUserServant userServant)
        {
            CompetenceServant = compentenceServant;
            UserServant = userServant;
        }

        [HttpGet, Route("")]
        public HttpResponseMessage GetMatches()
        {
            var userMatches = CompetenceServant.GetUserCompetences(CurrentUserId).Select(p => p.Id).ToList();
            var matchedUsers = CompetenceServant.GetMatches(CurrentUserId, userMatches, NUMBER_OF_MATCHES_TO_TAKE);

            var users = matchedUsers.Select(user => new UserResponse
            {
                UserId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Description = user.Description,
                UserLocation = user.UserLocation.ToDescription(),
                ProfileImageUrl = UserServant.GetProfileImageUrl(user.Id),
                Competences = CompetenceServant.GetUserCompetences(user.Id).Select(x => new GuidIdAndNameResponse { Id = x.Id, Name = x.Name }).ToList()
            });
            return CreateApiResponse(HttpStatusCode.OK, users);
        }        
    }
}
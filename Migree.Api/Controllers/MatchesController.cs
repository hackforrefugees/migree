using Migree.Api.Models;
using Migree.Api.Models.Responses;
using Migree.Core.Interfaces;
using Migree.Core.Models.Language;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("matches")]
    public class MatchesController : MigreeApiController
    {
        private const int NUMBER_OF_MATCHES_TO_TAKE = 20;

        private ICompetenceServant CompetenceServant { get; }
        private IUserServant UserServant { get; }        
        private ILanguageServant LanguageServant { get; }

        public MatchesController(ICompetenceServant compentenceServant, IUserServant userServant, ILanguageServant languageServant)
        {
            CompetenceServant = compentenceServant;
            UserServant = userServant;
            LanguageServant = languageServant;
        }

        [HttpGet, Route("")]
        public HttpResponseMessage GetMatches()
        {
            var userMatches = CompetenceServant.GetUserCompetences(CurrentUserId).Select(p => p.Id).ToList();
            var matchedUsers = CompetenceServant.GetMatches(CurrentUserId, userMatches, NUMBER_OF_MATCHES_TO_TAKE);

            var adminUser = UserServant.GetAdminUser();

            if (adminUser != null)
            {
                matchedUsers.Add(adminUser);
            }

            var users = matchedUsers.Select(user => new UserResponse
            {
                UserId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Description = user.Description,
                UserLocation = LanguageServant.Get<Definition>().UserLocation[user.UserLocation.ToString()],
                ProfileImageUrl = UserServant.GetProfileImageUrl(user.Id, user.HasProfileImage),
                Competences = CompetenceServant.GetUserCompetences(user.Id).Select(x => new GuidIdAndName { Id = x.Id, Name = x.Name }).ToList()
            });
            return CreateApiResponse(HttpStatusCode.OK, users);            
        }        
    }
}
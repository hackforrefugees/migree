using Migree.Api;
using Swashbuckle.Application;
using System.Web.Http;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Migree.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {            
            GlobalConfiguration.Configuration
                .EnableSwagger(c => { c.SingleApiVersion("v1", "Migree.Api"); })
                .EnableSwaggerUi(c => { });
        }
    }
}

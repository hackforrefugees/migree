using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Migree.AddCompetences
{
    class Program
    {
        static void Main(string[] args)
        {
            ///////"Java"
            //"C"
            //"C++"
            //"Python"
            //"C#"
            //"PHP"
            //"Visual Basic.NET"
            //"JavaScript"
            //"Perl"
            //"Ruby"
            //"Assembly language"
            //"Visual Basic"
            //"Delphi / Object Pascal"
            //"Swift"
            //"Objective - C"
            //"MATLAB"
            //"Pascal"
            //"R"
            //"PL / SQL"
            //"COBOL"

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["name"] = "Java";

                var response = client.UploadValues("http://localhost:50402/api/user/competences", values);

                var responseString = Encoding.Default.GetString(response);
            }
        }
    }
}

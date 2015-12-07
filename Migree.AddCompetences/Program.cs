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
            var competences = new List<string>();

            //Initial set of competences to add to the DB.
            competences.Add("C");
            competences.Add("Python");
            competences.Add("Visual Basic.NET");
            competences.Add("Perl");
            competences.Add("Ruby");
            competences.Add("Assembly language");
            competences.Add("Visual Basic");
            competences.Add("Delphi / Object Pascal");
            competences.Add("Swift");
            competences.Add("Objective - C");
            competences.Add("MATLAB");
            competences.Add("Pascal");
            competences.Add("R");
            competences.Add("SQL");
            competences.Add("COBOL");

            //Note - We're not checking to see if the competence already exists in the DB!
            using (var client = new WebClient())
            {
                foreach (var competence in competences)
                {
                    var values = new NameValueCollection();
                    values["name"] = competence;

                    var response = client.UploadValues("http://localhost:50402/competence", values);

                    var responseString = Encoding.Default.GetString(response);
                }
            }
        }
    }
}

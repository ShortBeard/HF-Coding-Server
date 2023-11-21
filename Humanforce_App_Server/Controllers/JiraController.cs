using Humanforce_App_Server.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace Humanforce_App_Server
{
    public class JiraController : ApiController
    {

        private string validUserEmail = "user@humanforce.com";
        private string validUserKey = "12345ABCEF";

        [HttpGet]
        public JiraIssues GetJiras() {

            var headers = Request.Headers;
            JiraIssues items = new JiraIssues();

            if (IsAuthorized(headers)) {
                string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "data/JiraAPI/backlog.json");
                using (StreamReader r = new StreamReader(dataFolderPath)) {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<JiraIssues>(json);
                    items.requestSuccess = true;
                }
            }
            else {
                items.requestSuccess = false;
            }

            return items;

        }

        [HttpPost]
        public JiraIssues SearchJiras() {
            JiraIssues items = new JiraIssues();
            var headers = Request.Headers;

            if (IsAuthorized(headers)) {
                string bodyContent = Request.Content.ReadAsStringAsync().Result;
                NameValueCollection queryParameters = HttpUtility.ParseQueryString(bodyContent); //Parse the key/values 

                string jql = queryParameters["jql"];
                int maxResults = int.Parse(queryParameters["maxResults"]);
                int startAt = int.Parse(queryParameters["startAt"]);

                //Get string between single quotes in query, i.e "SCRUM" from "Sprint='SCRUM'"
                string jqlParsed = "";
                Match match = Regex.Match(jql, @"'([^']*)");
                if (match.Success) {
                    jqlParsed = match.Groups[1].Value;
                }

                string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "data/JiraAPI/backlog.json");
                using (StreamReader r = new StreamReader(dataFolderPath)) {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<JiraIssues>(json);

                    //Filter results
                    items.issues = items.issues.Where(issue => issue.key.Contains(jqlParsed)).ToList(); //Anything with the key containing string specified in JQL query
                    items.issues = items.issues.Skip(startAt).ToList(); //Start at
                    items.issues = items.issues.Take(maxResults).ToList(); //Truncate by max results

                    items.total = items.issues.Count;
                    items.requestSuccess = true;
                    return items;


                }

            }
            return items;
        }

        private bool IsAuthorized(HttpRequestHeaders headers) {
            if (headers.Contains("Authorization")) {
                var authHeader = headers.GetValues("Authorization").FirstOrDefault();
                string authBase64 = authHeader.Replace("Basic ", "");
                string decodedString = DecodeBase64(authBase64);
                string[] authDetails = decodedString.Split(new char[] { ':' }, 2);
                string email = authDetails[0];
                string key = authDetails[1];

                return email == validUserEmail && key == validUserKey;
            }

            return false;
        }


        private string DecodeBase64(string value) {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            var valueBytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }
}

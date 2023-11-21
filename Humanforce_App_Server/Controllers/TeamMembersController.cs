using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;


namespace Humanforce_App_Server
{
    public class TeamMembersController : ApiController
    {

        [HttpGet]
        public List<TeamMembers> GetTeamMembers()
        {
            string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "data/TeamMembers.json");
            List<TeamMembers> teamMembers = new List<TeamMembers>();
            using (StreamReader r = new StreamReader(dataFolderPath))
            {
                string json = r.ReadToEnd();
                teamMembers = JsonConvert.DeserializeObject<List<TeamMembers>>(json);
            }

            return teamMembers;
        }
        
    }
}

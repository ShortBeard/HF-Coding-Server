using Newtonsoft.Json;
using System;
using System.IO;
using System.Web.Http;


namespace Humanforce_App_Server
{
    public class SprintsController : ApiController
    {

        [HttpGet]
        public Sprints GetSprints()
        {
            string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "data/JiraAPI/sprints.json");
            Sprints sprints = new Sprints();
            using (StreamReader r = new StreamReader(dataFolderPath))
            {
                string json = r.ReadToEnd();
                sprints = JsonConvert.DeserializeObject<Sprints>(json);
            }

            return sprints;
        }
        
    }
}

using Newtonsoft.Json;
using System;
using System.IO;
using System.Web.Http;


namespace Humanforce_App_Server
{
    public class PublicHolidayController : ApiController
    {

        [HttpGet]
        public PublicHolidayItems GetPublicHolidays(string countryName)
        {
            string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "data/GoogleCalendarAPI/" + countryName + ".json");
            PublicHolidayItems items = new PublicHolidayItems();
            using (StreamReader r = new StreamReader(dataFolderPath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<PublicHolidayItems>(json);
            }

            return items;
        }

    }
}

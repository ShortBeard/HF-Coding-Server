using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Humanforce_App_Server
{
    internal class Program
    {
        static void Main()
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8081");

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            var cors = new EnableCorsAttribute("*", "*", "*"); //Allow requests from angular app on different port
            config.EnableCors(cors);

            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}

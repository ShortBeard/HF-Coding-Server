using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanforce_App_Server
{
    public class PublicHoliday
    {
        public string summary;
        public PublicHolidayDate start;
        public PublicHolidayDate end;

    }

    public class PublicHolidayItems {
        public List<PublicHoliday> items;
    }

    public class PublicHolidayDate {
        public string date;
    }
}

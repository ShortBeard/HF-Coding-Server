using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanforce_App_Server
{
    public class Sprints
    {
        public List<SprintsValues> values;

    }

    public class SprintsValues {
        public int id;
        public string name;
        public string startDate;
        public string endDate;
    }

}

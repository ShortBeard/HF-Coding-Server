using System.Collections.Generic;

namespace Humanforce_App_Server.Models
{

    public class Jira
    {
        public string id;
        public string key;
        public JiraFields fields;
    }

    public class JiraIssues
    {
        public bool requestSuccess; //Determines if the request was valid 
        public int total;
        public List<Jira> issues;
    }


    public class JiraFields {
        public string customfield_10016; //Story Points
        public JiraSprint[] customfield_10020; //Stores sprint information about this jira?
                                               //Backlog states are all "future" while sprint files shows "closed", so this is just an assumption for the purpose of this project.
    }

    public class JiraSprint {
        public int id;
    }
}

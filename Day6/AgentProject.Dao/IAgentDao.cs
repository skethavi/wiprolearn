using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentProject.Models;

namespace AgentProject.Dao
{
    internal interface IAgentDao
    {
        string AddAgentDao(Agent agent);
        List<Agent> ShowAgentDao();
        Agent SearchAgentDao(int agentId);
        string UpdateAgentDao(Agent agentUpdated);
        string DeleteAgentDao(int agentId);
        string WriteToFileDao();
        string ReadFromFileDao();
    }
}

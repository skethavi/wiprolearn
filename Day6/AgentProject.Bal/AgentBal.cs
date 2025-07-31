using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentProject.Models;
using AgentProject.Exceptions;
using AgentProject.Dao;

namespace AgentProject.Bal
{
    public class AgentBal
    {
        public StringBuilder sb = new StringBuilder();
        public static AgentDaoImpl daoImpl;

        public object Gender { get; private set; }

        static AgentBal()
        {
            daoImpl = new AgentDaoImpl();
        }

        public List<Agent> ShowAgentBal()
        {
            return daoImpl.ShowAgentDao();
        }

        public string WriteFileBal()
        {
            return daoImpl.WriteToFileDao();
        }

        public string ReadFileBal()
        {
            return daoImpl.ReadFromFileDao();
        }

        public string DeleteAgentBal(int agentId)
        {
            return daoImpl.DeleteAgentDao(agentId);
        }

        public string UpdateAgentBal(Agent agentUpdated)
        {
            if (ValidateAgent(agentUpdated) == true)
            {
                return daoImpl.UpdateAgentDao(agentUpdated);
            }
            throw new AgentException(sb.ToString());
        }

        public Agent SearchAgentBal(int agentId)
        {
            return daoImpl.SearchAgentDao(agentId);
        }

        public string AddAgentBal(Agent agent)
        {
            if (ValidateAgent(agent) == true)
            {
                return daoImpl.AddAgentDao(agent);
            }
            throw new AgentException(sb.ToString());
        }




        public bool ValidateAgent(Agent agent)
        {
            bool flag = true;
            if (agent.FirstName.Length < 3)
            {
                sb.Append("FirstName Contains Min. 3 characters...\n");
                flag = false;
            }
            if (agent.LastName.Length < 3)
            {
                sb.Append("LastName Contains Min. 3 characters...\n");
                flag = false;
            }
            if (agent.PremiumAmount <= 10000)
            {
                sb.Append("PremiumAmount Must be Greater than 10000...\n");
                flag = false;
            }
            return flag;
        }
    }
}

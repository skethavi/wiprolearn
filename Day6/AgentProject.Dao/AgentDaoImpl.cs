using AgentProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AgentProject.Dao
{
    public class AgentDaoImpl : IAgentDao
    {
        static List<Agent> agentList;

        static AgentDaoImpl()
        {
            agentList = new List<Agent>();
        }

        public int GenerateAgentId()
        {
            if (agentList.Count == 0)
            {
                return 1;
            }
            else
            {
                int agentId = agentList.ElementAt(agentList.Count - 1).AgentId;
                agentId++;
                return agentId;
            }
        }
        public string AddAgentDao(Agent agent)
        {
            int id = GenerateAgentId();
            agent.AgentId = id;
            agentList.Add(agent);
            return "Agent Record Inserted...";
        }

        public string DeleteAgentDao(int agentId)
        {
            Agent agentFound = SearchAgentDao(agentId);
            if (agentFound != null)
            {
                agentList.Remove(agentFound);
                return " Agent Record Deleted Successfully....";
            }
            return "Agent Record Not Found...";
        }


        public string ReadFromFileDao()
        {
            FileStream fs = new FileStream(@"C:\file", FileMode.Open, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            agentList = (List<Agent>)formatter.Deserialize(fs);
            return "Data Retrieved from the File Successfully...";
        }

        public Agent SearchAgentDao(int agentId)
        {
            Agent agentFound = null;
            foreach (Agent agent in agentList)
            {
                if (agent.AgentId == agentId)
                {
                    agentFound = agent;
                    break;
                }
            }
            return agentFound;
        }

        public List<Agent> ShowAgentDao()
        {
            return agentList;
        }

        public string UpdateAgentDao(Agent agentUpdated)
        {
            Agent agentFound = SearchAgentDao(agentUpdated.AgentId);
            if (agentFound != null)
            {
                agentFound.FirstName = agentUpdated.FirstName;
                agentFound.LastName = agentUpdated.LastName;
                agentFound.City = agentUpdated.City;
                agentFound.Gender = agentUpdated.Gender;
                agentFound.PremiumAmount = agentUpdated.PremiumAmount;
                return "Agent Record Updated...";
            }
            return "Agent Record Not Found...";
        }

        public string WriteToFileDao()
        {
            FileStream fs = new FileStream(@"C:\file", FileMode.Create, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, agentList);
            fs.Close();
            return "Data Stored in Files Successfully...";
        }
    }
    
    
}

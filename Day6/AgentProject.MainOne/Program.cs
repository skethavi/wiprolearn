using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentProject.Models;
using AgentProject.Bal;
using AgentProject.Exceptions;

namespace AgentProject.MainOne
{
    internal class Program
    {
        static AgentBal agentBal;
        static Program()
        {
            agentBal = new AgentBal();
        }

        public static void WriteFileMain()
        {
            Console.WriteLine(agentBal.WriteFileBal());
        }

        public static void ReadFileMain()
        {
            Console.WriteLine(agentBal.ReadFileBal());
        }

        public static void DeleteAgentMain()
        {
            int agentId;
            Console.WriteLine("Enter AgentId   ");
            agentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(agentBal.DeleteAgentBal(agentId));

        }

        public static void UpdateAgentMain()
        {
            Agent agent = new Agent();
            Console.WriteLine("AgentId   ");
            agent.AgentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Agent FirstName  ");
            agent.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Agent LastName  ");
            agent.LastName = Console.ReadLine();
            Console.WriteLine("Enter Agent City  ");
            agent.City = Console.ReadLine();
            Console.WriteLine("Enter Gender (MALE/FEMALE)   ");
            agent.Gender = Console.ReadLine();
            Console.WriteLine("Enter PremiumAmount  ");
            agent.PremiumAmount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine(agentBal.AddAgentBal(agent));
        }


        public static void SearchAgentMain()
        {
            int agentId;
            Console.WriteLine("Enter AgentId    ");
            agentId = Convert.ToInt32(Console.ReadLine());
            Agent agent = new Agent();
            if (agentId != null)
            {
                Console.WriteLine(agent);
            }
            else
            {
                Console.WriteLine("*** Agent Record Not Found ***");
            }
        }



        public static void ShowAgentMain()
        {
            List<Agent> agentList = agentBal.ShowAgentBal();
            Console.WriteLine("Employ Record Are   ");
            foreach (Agent agent in agentList)
            {
                Console.WriteLine(agent);
            }
        }

        public static void AddAgentMain()
        {
            Agent agent = new Agent();
            Console.WriteLine("AgentId   ");
            Console.WriteLine("Enter Agent FirstName  ");
            agent.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Agent LastName  ");
            agent.LastName = Console.ReadLine();
            Console.WriteLine("Enter Agent City  ");
            agent.City = Console.ReadLine();
            Console.WriteLine("Enter Gender (MALE/FEMALE)   ");
            agent.Gender = Console.ReadLine();
            Console.WriteLine("Enter PremiumAmount  ");
            agent.PremiumAmount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine(agentBal.AddAgentBal(agent));
        }
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.WriteLine("O P T I O N S");
                Console.WriteLine("-------------");
                Console.WriteLine("1. Add Agent");
                Console.WriteLine("2. Show Agent");
                Console.WriteLine("3. Search Agent");
                Console.WriteLine("4. Update Agent");
                Console.WriteLine("5. Delete Agent");
                Console.WriteLine("6. Write to File");
                Console.WriteLine("7. Read From File");
                Console.WriteLine("8. Exit");
                Console.WriteLine("Enter Your Choice  ");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        try
                        {
                            AddAgentMain();
                        }
                        catch (AgentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 2:
                        ShowAgentMain();
                        break;
                    case 3:
                        SearchAgentMain();
                        break;
                    case 4:
                        try
                        {
                            UpdateAgentMain();
                        }
                        catch (AgentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 5:
                        DeleteAgentMain();
                        break;
                    case 6:
                        WriteFileMain();
                        break;
                    case 7:
                        ReadFileMain();
                        break;
                    case 8:
                        return;
                }
            } while (choice != 8);

        }
    }
}

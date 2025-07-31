using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentProject.Models
{
    [Serializable]
    public class Agent
    {
        static int agentId = 1;
        public int AgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public decimal PremiumAmount { get; set; }

        public Agent() { }
        public Agent(string firstName, string lastName, string city, string gender, decimal premiumAmount)
        {
            AgentId = agentId++;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Gender = gender;
            PremiumAmount = premiumAmount;
        }

        public override string ToString()
        {
            return $"AgentId: {AgentId}\nFirstName: {FirstName}\nCity:{City}\nGender: {Gender}\nPremiumAmount: {PremiumAmount} ";

        }
    }
}
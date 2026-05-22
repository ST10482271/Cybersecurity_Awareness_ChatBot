using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cybersecurity_Awareness_ChatBot_2
{
    public class ChatbotMemory
    {
        private static string userFile = "users.txt";
        private static string interestFile = "interests.txt";

        public static string GetReturningUserGreeting(string userName)//returns a greeting for a returning user
        {

            //check if file exists
            if (!File.Exists(userFile))
            {
                return " ";
            }

            List<string> savedTopics = new List<string>();//list to store the saved topics
            string[] lines = File.ReadAllLines(interestFile);//read all lines from the interest file

            foreach (string line in lines)
            {
                string[] parts = line.Split(':');//split the line into parts using ':' as the delimiter
                if (parts.Length == 2 && parts[0].Equals(userName, StringComparison.OrdinalIgnoreCase))//check if the first part of the line matches the userName (case-insensitive)
                {
                    savedTopics.Add(parts[1].Trim());//if it matches, add the second part (the topic) to the savedTopics list
                }
            }

            if (savedTopics.Count > 0) { 
            
                string topicsList = string.Join(", ", savedTopics);//join the saved topics into a single string separated by commas
                return $" SUPRISE!! I remebered you ;) Welcome back! {userName}! Last time we talked alot about OR you were interested in: {topicsList}. What would you like to discuss today?";//return a greeting message that includes the user's name and the list of saved topics
            }

            return ""; //if there are no saved topics, return an empty string

        }

        public static void SaveUserInterest(string userName, string topic)//saves the user's interest to a file
        {
           string record = $"{userName}:{topic}";//create a record string in the format "userName:topic"
            
            //see if file exists
            if(!File.Exists(interestFile)) File.Create(interestFile).Close();//if the file does not exist, create it and close it immediately
            if (!File.Exists(userFile)) File.Create(userFile).Close();//if the file does not exist, create it and close it immediately

            //read all files to check for duplicates
            List<string> existingInterests = File.ReadAllLines(interestFile).ToList();//read all lines from the interest file into a list

            //if this specific user and topic combination already exists, do not save it again
            if(!existingInterests.Contains(record, StringComparer.OrdinalIgnoreCase))//check if the record already exists in the existingInterests list (case-insensitive)
            {
                File.AppendAllText(interestFile, record + Environment.NewLine);//if it does not exist, append the record to the interest file with a new line

                //also save the user name to the user file if it is not already there
                List<string> existingUsers = File.ReadAllLines(userFile).ToList();//read all lines from the user file into a list
                if (!existingUsers.Contains(userName, StringComparer.OrdinalIgnoreCase))//check if the userName already exists in the existingUsers list (case-insensitive)
                {
                    File.AppendAllText(userFile, userName + Environment.NewLine);//if it does not exist, append the userName to the user file with a new line
                }
            }
        }


    }
}

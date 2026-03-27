using System;

namespace Cybersecurity_Awareness_ChatBot
{
    public class Response_System_and_Validation
    {
        public void Responses()
        {

            string response = " "; 

            string[] keyWords = { "password", "phishing", "browsing", };//Array of keywords that the chatbot will recognize and respond to

            string[] responseArray = { "Passwords should be 8 characters\n -Should include:\n" +
                    "  -Captial letters \n  -Lowercase letters \n  -Special characters e.g. # \n  -Numbers\n" +
                    "-Should be changed every 3-6 months depending on importance", "Phishing is a form of social engineering where the attacker provides what" +
                    "appers to be a legitimate communication e.g. an e-mail, but it contains hidden or embedded" +
                    "code that redirects the reply to a third party site in an effort to extract personal" +
                    "confidential infomation", "Safe browsing is the practice of using the internet in a way that minimizes the risk of" +
                    "encountering malicious websites, scams, and other online threats. It involves being cautious about the websites you visit," +
                    "the links you click on, and the information you share online. Safe browsing practices include:" +
                    "\n-Using a secure web browser with built-in security features \n-Keeping your browser and plugins up to date" +
                    " \n-Avoiding clicking on suspicious links or downloading files from untrusted sources \n-Using strong, unique passwords for online accounts" +
                    " \n-Enabling two-factor authentication (2FA) for added security" };//Array of responses that correspond to the keywords in the keyWords array

            bool found = false;//Boolean variable to track whether a keyword was found in the user's response

            Console.WriteLine("Welcome to the chatbot, When you feel like ending our conversion please enter '@");//Welcome message to the user, instructing them on how to end the conversation
            Console.WriteLine();

            while (true)//Infinite loop to continuously prompt the user for input until they choose to end the conversation
            {
                Console.ForegroundColor = ConsoleColor.Cyan;//Set the console text color to cyan for the user's input
                Console.Write($"{Program.SavedName}: ");//Prompt the user for input, displaying their saved name as part of the prompt
                response = Console.ReadLine().ToLower();//string manipulation to convert the user's input to lowercase, making it easier to compare with the keywords in the keyWords array
                Console.WriteLine();
                Console.ResetColor();//Reset the console text color to the default color after the user has entered their response


                string[] seprated = response.Split('?','.',' ');//Split the user's response into individual words and store them in an array also using the specified delimiters (question mark, period, and space) to separate the words.

                foreach (string s in seprated)//Iterate through each word in the user's response and check if it matches any of the keywords in the keyWords array
                {

                    for (int i = 0; i < keyWords.Length; i++)//Loop through the keyWords array to compare each keyword with the current word from the user's response
                    {
                        if (s == keyWords[i])
                        {
                           Print($"CSA Bot: { responseArray[i]}");
                            found = true;//If a match is found, print the corresponding response from the responseArray and set the found variable to true
                        }

                    }


                }

                Console.WriteLine();
               
                if (keyWords[0] == response )///Check if the user's response exactly matches the first keyword in the keyWords array (in this case, "password") and if so, provide additional information by sharing a YouTube video link related to password safety
                {
                    string url = "https://www.youtube.com/watch?v=aEmF3Iylvr4";
                    Console.WriteLine($"Here is a YouTube video to give you more info about your search: {url} ");

                    Print("Press '0' to watch video or any other key to continue our conversion");//input validation to allow the user to choose whether they want to watch the video or continue the conversation without watching it
                    Console.WriteLine();

                    if (Console.ReadKey().Key == ConsoleKey.D0)//Check if the user presses the '0' key, and if so, attempt to open the provided URL in the default web browser
                    {
                        try {
                            System.Diagnostics.Process.Start(url);//Attempt to open the URL in the default web browser
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Print("An error occurred while trying to open the URL: " + ex.Message);//If an error occurs while trying to open the URL, catch the exception and print an error message to the console
                            Console.ResetColor();
                        }
                        
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();

                if (response == "@")//Check if the user wants to end the conversation by entering '@'
                {
                    Print("Thank you for engaing with me, I hope you learnt something usefull and I was able to assit you :)");
                    break;
                }


                if (!found)//If no keywords were found in the user's response, print a message indicating that the chatbot did not understand the input and prompt the user to rephrase their question
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Print("I do not understand, could you please question me with the concepts" +
                    " I understand. Could you please rephrase your question? NB! @ to end our conversion. ");
                    Console.ResetColor();
                    Console.WriteLine();
                }
               
            }

        }

        public void Print(string text, int speed =75){//Method to print text to the console with a typing effect, where each character is printed with a delay between them. The speed parameter controls the delay time in milliseconds (default is 75ms).

            foreach (Char c in text) {

                Console.Write(c);
                System.Threading.Thread.Sleep(speed);//Pause the execution of the program for a specified amount of time (in milliseconds) after printing each character, creating a typing effect when displaying text in the console
            }
            Console.WriteLine();
        } 

    }

                         
}


    

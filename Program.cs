using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Cybersecurity_Awareness_ChatBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Response_System_and_Validation Response_System_and_Validation_Obj = new Response_System_and_Validation();// Create an instance of the Response_System_and_Validation class to access its methods and functionality, allowing the chatbot to process user input and generate appropriate responses based on the defined logic within the class.
            //Create sound greeting message to welcome users to the chatbot
            PlaySound();

            //Display chatbot logo
            DisplayLogo();

            //Greet user and display welcome message
            GreetUser();

           
            Response_System_and_Validation_Obj.Responses();//Call the Responses method to process the user's input and generate an appropriate response
            Console.WriteLine();

            
        }

        public static void PlaySound()
        {
            //Create a SoundPlayer instance and set the sound location to the desired audio file
            var soundPlayer = new System.Media.SoundPlayer();
            soundPlayer.SoundLocation = @"C:\Users\Student\Desktop\Recording.wav";
            soundPlayer.PlaySync();
        }

        public static void DisplayLogo()
        {
            Console.ForegroundColor = ConsoleColor.Red;//Set the console text color to red for the logo
            //Create a simple ASCII art logo for the chatbot
            Console.WriteLine(@"
              |`-._/\_.-`|
              |    C-S   |
              |___o()o___|
              |__((<>))__|
              \   o\/o   /
               \   A-B  /
                \  ||  /
                 '.||.'
                   ``


");
                Console.ResetColor();
        }

        public static void GreetUser()
        {
            Response_System_and_Validation Response_System_and_Validation_Obj = new Response_System_and_Validation();
            Console.ForegroundColor = ConsoleColor.Green;//Set the console text color to yellow for the welcome banner
                                                         //Display a welcome banner to the user
            Console.WriteLine(
                @"╔~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^-╗
│                                                                     │
│        Welcome to the Cybersecurity Awareness ChatBot!              │
│                                  *                                  │
│                                 CSAB                                │                    
╚~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~╝");
            Console.ResetColor();

            Console.WriteLine();

            string name = GetUserName();//Call the GetUserName method to prompt the user for their name and store it in a variable

            Console.WriteLine();
            Response_System_and_Validation_Obj.Print($"CSA Bot: Hello {name}! , I hope you are well. \nMy purpose is to educate you with cybersecurity concepts such as: \n*Password Safety \n*Phishing \n*Safe browsing");//Greet the user by name and let users know what the chatbot is designed to do. 
            Console.WriteLine();
        }

        public static string SavedName { get; private set; }//Define a property to store the user's name for later use in the chatbot's responses and interactions.
        //preventes repeating the method or validation for getting the user's name multiple times throughout the chatbot's interactions.

        public static string GetUserName() //Define a method to get the user's name with input validation
        {
            Response_System_and_Validation Response_System_and_Validation_Obj = new Response_System_and_Validation();

            Response_System_and_Validation_Obj.Print("Please enter your name: ");
          
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;//Set the console text color to cyan for the user's input
            Console.Write("You: ");
            string name = Console.ReadLine();//Read the user's input and store it in a variable
            Console.WriteLine();
            Console.ResetColor();//Reset the console text color to the default color after reading the user's input

            while (true)
            {

                if (String.IsNullOrWhiteSpace(name))//Check if the user's input is null, empty, or consists only of whitespace characters(input validation)
                {
                    Console.ForegroundColor = ConsoleColor.Red;//Set the console text color to red for the error message
                    Console.WriteLine();
                    Response_System_and_Validation_Obj.Print($"CSA Bot:The field for name can not be empty!");
                    Console.WriteLine();

                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("You: ");
                    name = Console.ReadLine();
                    Console.ResetColor();
                    continue;
                }
                if (name.Length < 2)//Check if the user's input is less than 2 characters long (input validation)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Response_System_and_Validation_Obj.Print($"CSA Bot:The name should be at least 2 characters long!");
                    Console.WriteLine();

                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("You: ");
                    name = Console.ReadLine();
                    Console.ResetColor();
                    continue;
                }
                if (!Regex.IsMatch(name, @"[a-zA-z]+$"))//Check if the user's input contains only letters (input validation)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Response_System_and_Validation_Obj.Print($"CSA Bot:The name should only contain letters!");
                    Console.WriteLine();

                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("You: ");
                    name = Console.ReadLine();
                    Console.ResetColor();
                    continue;//If the user's input fails any of the validation checks, display an appropriate error message and prompt the user to enter their name again until a valid name is provided.
                }
                break;//If the user's input passes all validation checks, exit the loop and proceed with the valid name.


            }
            SavedName = name;//Store the valid name in the SavedName property for later use in the chatbot's responses and interactions
            return name;//Return the valid name to be used in the GreetUser method for greeting the user by name and providing a personalized experience in the chatbot.

        }




    }

    


}


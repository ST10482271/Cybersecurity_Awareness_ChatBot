using System;
using System.Text.RegularExpressions;

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
        }

        public static void GreetUser()
        {
            Response_System_and_Validation Response_System_and_Validation_Obj = new Response_System_and_Validation();
            //Display a welcome banner to the user
            Console.WriteLine(
                @"╔~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^-╗
│                                                                     │
│        Welcome to the Cybersecurity Awareness ChatBot!              │
│                                  *                                  │
│                                 CSAB                                │                    
╚~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~╝");

            Console.WriteLine();

            Console.WriteLine("Please enter your name: ");//Prompt the user to enter their name

            Console.WriteLine();

            string name = Console.ReadLine();//Read the user's input and store it in a variable
            Console.WriteLine();

            while (true)
            {

                if (String.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("The field for name can not be empty or ");
                    name = Console.ReadLine();
                    continue;
                }
                if (name.Length < 2)
                {
                    Console.WriteLine("The name should be at least 2 characters long");
                    name = Console.ReadLine();
                    continue;
                }
                if (!Regex.IsMatch(name, @"[a-zA-z]+$"))
                {
                    Console.WriteLine("The name should only contain letters");
                    name = Console.ReadLine();
                    continue;
                }
                break;
            }

            Console.WriteLine($"Hello {name}! , I hope you are well. \nMy purpose is to educate you with cybersecurity concepts such as: \n*Password Safety \n*Phishing \n*Safe browsing");//Greet the user by name and let users know what the chatbot is designed to do. 
            Console.WriteLine();
                       

        }




    }

    


}


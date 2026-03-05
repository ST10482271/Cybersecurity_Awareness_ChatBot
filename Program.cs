using System;

namespace Cybersecurity_Awareness_ChatBot
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Create sound greeting message to welcome users to the chatbot
            PlaySound();

            //Display chatbot logo
            DisplayLogo();

            //Greet user and display welcome message
            GreetUser();
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

            Console.WriteLine($"Hello {name}!");//Greet the user by name


        }




    }

   


}

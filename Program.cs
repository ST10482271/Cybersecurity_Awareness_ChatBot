using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Cybersecurity_Awareness_ChatBot
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            //Create sound greeting message to welcome users to the chatbot
            PlaySound();

        }

        public static void PlaySound() { 
        
            var soundPlayer = new System.Media.SoundPlayer();
            soundPlayer.SoundLocation = @"C:\Users\Student\Desktop\Recording.wav";
            soundPlayer.PlaySync();
        }

        
    }
}

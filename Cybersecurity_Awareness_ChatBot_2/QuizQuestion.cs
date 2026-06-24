using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Awareness_ChatBot_2
{
    public class QuizQuestion
    {
        //QuizQuestion object properties
        public string QuestionText { get; set; }
        public string ChoiceA { get; set; }
        public string ChoiceB { get; set; }
        public string ChoiceC { get; set; }
        public string CorrectAnswer { get; set; } // Will store "A", "B", or "C"
        public string Explanation { get; set; }
    }
}

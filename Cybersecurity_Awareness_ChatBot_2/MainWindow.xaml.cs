using Microsoft.VisualBasic;
using System;
using System.Diagnostics.Eventing.Reader;
using System.IO.Packaging;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static Microsoft.Data.SqlClient.Internal.SqlClientEventSource;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;

namespace Cybersecurity_Awareness_ChatBot_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string[] password =
         {
            "A password is a confidential sequence of characters (letters, numbers, and symbols) used by a user to prove their identity to a system.",
            "A password is a security measure designed to grant authorized users access to a device, application, or network while restricting unauthorized users.",
            "A password is a specific type of shared secret (similar to a passphrase) meant to be known only to the user and the system, often considered something you know in authentication protocols.",
            "A password is a modern watchword or code, often used with a username, to verify that a person is allowed to enter a digital space, similar to a physical key or a password given to a guard",
            "A password is a line of defense designed to protect personal, financial, or confidential information from being stolen by malicious actors (hackers)." };
        string[] scams =
        {
            "A scam is a fraudulent scheme or deceptive practice designed to trick individuals into giving away money, personal information, or other valuable assets.",
            "A scam is a  type of cyber attack where attackers impersonate legitimate entities (like banks, government agencies, or well-known companies) to deceive victims into providing sensitive information or making financial transactions.",
            "A scam is a form of online fraud that often involves fake websites, emails, or messages that appear to be from trustworthy sources, aiming to steal personal data or money from unsuspecting victims.",
            "A scam is a situation where you should never send money or provide personal information to unexpected requests. Independently verify the claim by contacting the company directly using a trusted, official phone number or website rather than the contact info provided in the message.",
            "A scam is a scenario where you should treat your sensitive information like cash. Never share your passwords, PINs, or One-Time Passwords (OTPs) with anyone, and be cautious about how much personal detail you share on social media."
        };

        string[] privacy =
        {
            "Privacy is the state or condition of being free from public attention or unsanctioned intrusion, especially in the context of personal information and online activities.",
            "Privacy is the right of individuals to control access to their personal information and to be free from unauthorized surveillance or data collection.",
            "Privacy is a fundamental aspect of cybersecurity that involves protecting sensitive data from unauthorized access, ensuring that individuals have control over their own information, and maintaining confidentiality in digital interactions.",
            "Privacy is a modern watchword or code, often used with a username, to verify that a person is allowed to enter a digital space, similar to a physical key or a password given to a guard",
            "Privacy is a line of defense designed to protect personal, financial, or confidential information from being stolen by malicious actors (hackers)."
        };

        string[] phishing =
        {
            "Phishing is a type of cyber attack where attackers attempt to trick individuals into providing sensitive information, such as usernames, passwords, or financial details, by pretending to be a trustworthy entity.",
            "Phishing attacks often come in the form of emails, messages, or websites that appear legitimate but are designed to steal personal information.",
            "Phishing is a common method used by cybercriminals to gain unauthorized access to accounts, commit identity theft, or carry out financial fraud by exploiting human psychology and trust.",
            "To protect yourself from phishing, be cautious of unsolicited messages, verify the sender's identity, and avoid clicking on suspicious links or downloading attachments from unknown sources.",
            "To protect yourself from phishing look up the company’s official phone number or website on a previous statement or official directory, and contact them yourself."
        };

        string userName = " ";
        string input = " ";
        string reply = " ";
        string currentTopic = " ";
        string currentUsername = " ";

        List<string> sessionTopics = new List<string>();
        bool checkedGreeting = false;

        TasksRepo repo = new TasksRepo();
        LogActivity logRepo = new LogActivity();

        private enum ChatState { Default, AddingTask, TakingQuiz }// Enum to represent the current state of the chatbot, which can be in a default state, adding a task, or taking a quiz
        private ChatState currentState = ChatState.Default;// Variable to keep track of the current state of the chatbot, initialized to the default state

        private List<QuizQuestion> quizQuestions = new List<QuizQuestion>();//this is the object that will hold the quiz questions, it is a list of QuizQuestion objects, which is a class that holds the question, choices, correct answer, and explanation for each question (getters and setters are used to access the properties of the class)
        private int currentQuestionIndex = 0;//acts a pointer to keep track of which question user is currrently on
        private int quizScore = 0;//acts a counter to keep track of how many questions the user has answered correctly, it is incremented each time the user answers a question correctly

        public MainWindow()
        {
            InitializeComponent();
            PlaySound();

            Chatbot_Color("CSABot: ", $"Welcome to the CSABot{currentUsername}, please enter '@' to end our conversation {Environment.NewLine} ");
            Chatbot_Color("CSABot: ", $"I am here to assit you with passwords, scams, privacy, phishing, {Environment.NewLine}");
            Chatbot_Color("CSABot: ", $"If you are a returing friend, please greet me with a 'hello' or 'hi' or press send to receive a surprise!!{Environment.NewLine}");//tooo make the chatbot more fun and engaing 
           
        }
        private void start_bot(object sender, RoutedEventArgs e)
        {//start
         //set the logo grid to Hidden
            logo_grid.Visibility = Visibility.Hidden;
            //set the username grid to visible
            username_grid.Visibility = Visibility.Visible;
        }//end 

        public static void PlaySound()
        {
            //try catch block to handle potential exceptions that may occur when trying to play the sound file, such as file not found or unsupported format
            try
            {
                System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer();
                string runningFolder = AppDomain.CurrentDomain.BaseDirectory;
                soundPlayer.SoundLocation = System.IO.Path.Combine(runningFolder, "Recording.wav");
                soundPlayer.Play();

            }
            catch(Exception ex) { 
                // Handle the exception (e.g., log it, show a message to the user, etc.)
               MessageBox.Show($"Error playing sound: {ex.Message}");
            }
           
        }


        public void SendButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the user's input from the InputArea TextBox and trim any leading or trailing whitespace
            input = InputArea.Text.Trim();
            userName = txtNameInput.Text.Trim();


            // Check if the input is the "@" symbol, which is reserved for ending the conversation, and if so, shut down the application
            if (input == "@")
            {
                Application.Current.Shutdown();
                return;
            }


            // Append the user's input to the ChatArea TextBox, prefixed with the user's name and followed by a new line for formatting
            User_Color($"{userName}: ", $"{input} {Environment.NewLine}");

            reply = ChatAiResponse(input);// Get the chatbot's response by calling the ChatAiResponse method with the user's input

            Chatbot_Color($"CSABot: ", $"{reply}{Environment.NewLine}");


            // Clear the InputArea TextBox, scroll the ChatScrollViewer to the end to show the latest messages, and set focus back to the InputArea for user convenience
            InputArea.Clear();
            ChatScrollViewer.ScrollToEnd();
            InputArea.Focus();
        }


        public string ChatAiResponse(string input)
        {
            input = input.Trim().ToLower();

            string[] splitted = input.Split(' ');

            while (true)
            {

                // Check for returning user greeting
                if (!checkedGreeting && !string.IsNullOrWhiteSpace(currentUsername))
                { //

                    checkedGreeting = true;// Ensure we only check for a greeting once per session
                    string greeting = ChatbotMemory.GetReturningUserGreeting(currentUsername);// Get a personalized greeting based on the user's name and past interactions
                    if (!string.IsNullOrWhiteSpace(greeting))
                    {
                        return greeting;// If a personalized greeting is available, return it to the user
                    }
                }

                // Detect topic based on keywords in the input
                string detectedTopic = "";
                if (input.Contains("passwords")) detectedTopic = "passwords";
                if (input.Contains("scams")) detectedTopic = "scams";
                if (input.Contains("privacy")) detectedTopic = "privacy";
                if (input.Contains("phishing")) detectedTopic = "phishing";

                if (!string.IsNullOrWhiteSpace(detectedTopic))
                {

                    sessionTopics.Add(detectedTopic);// Add the detected topic to the session topics list for potential future reference or analysis

                    // Count how many times the detected topic has been mentioned in the current session
                    int topicCount = 0;
                    foreach (string topic in sessionTopics)
                    {

                        if (topic == detectedTopic)
                        {
                            topicCount++;
                        }

                    }

                    //check if they said interested in learning about the topic or asked over 2 times
                    bool isInterested = input.Contains("interested");
                    if (isInterested || topicCount > 2)
                    {

                        ChatbotMemory.SaveUserInterest(currentUsername, detectedTopic);// Save the user's interest in the detected topic for future reference or personalized interactions
                    }
                }

                // Check for empty input
                if (string.IsNullOrWhiteSpace(input))
                {
                    return $"I'm here to assist {currentUsername} Please type a message or ask a question about the topics provided above.";
                }

                //Set current topic 
                if (input.Contains("passwords")) currentTopic = "passwords";
                if (input.Contains("scams")) currentTopic = "scams";
                if (input.Contains("privacy")) currentTopic = "privacy";
                if (input.Contains("phishing")) currentTopic = "phishing";

                //Handle sentiemtnal responses
                Sentiemantal_Responses sentimentalResponse = new Sentiemantal_Responses();
                string response = sentimentalResponse.GetSentimentalResponse(input, currentUsername);

                if (!string.IsNullOrWhiteSpace(response))
                {

                    return response;
                }

                //if no sentiement normal responses
                if (input.Contains("passwords"))
                    return GetRandomTips(password);

                if (input.Contains("scams"))
                    return GetRandomTips(scams);

                if (input.Contains("privacy"))
                    return GetRandomTips(privacy);

                if (input.Contains("phishing"))
                    return GetRandomTips(phishing);


                // Handle requests for more information
                if (HasAnyKeyword(input, "explain", "another", "more"))
                {

                    if (currentTopic == "passwords") return GetRandomTips(password);//return more info on the current topic
                    if (currentTopic == "scams") return GetRandomTips(scams);
                    if (currentTopic == "privacy") return GetRandomTips(privacy);
                    if (currentTopic == "phishing") return GetRandomTips(phishing);

                }

                // Handle expressions of worry or concern
                if (HasAnyKeyword(input, "worried", "concerned", "fearful,", "frustrated"))
                {

                    return $"I know cybersecurity can be overwhelming, but being informed and cautious is the best way to protect yourself. Please specifiy which topic you are concerned about. e.g i am worried about passwords.";
                }

                // Handle requests to view activity logs, I used a database as i think i will work better than a list or dictionary,rgarding how the projec and data is structured, it is easier to store and retrieve, makes the project more dynamic
                if (input.Contains("show activity logs") || input.Contains("view logs") || input.Contains("activity logs"))
                {

                    List<string> logs = logRepo.GetActivityLog(currentUsername);
                    string totalLogs = "";

                    // Check if they actually have any logs yet
                    if (logs.Count == 0)
                    {
                        return "\nNo activity history found for your account.\n";

                    }

                    // Return the most recent log entry for the user
                    foreach (string logLine in logs)
                    {
                        totalLogs += logLine + "\n";
                    }
                    return $"\n{totalLogs}";

                }

                if (input.StartsWith("add task"))
                {
                    try
                    {
                        string[] taskParts = input.Split(',');

                        string title = taskParts[1].Trim();
                        string description = taskParts[2].Trim();


                        DateTime? reminder = null;
                        if (taskParts.Length > 3 && !string.IsNullOrWhiteSpace(taskParts[3]))
                        {
                            // Try to parse the reminder date, if provided, and handle potential format issues
                            if (DateTime.TryParse(taskParts[3].Trim(), out DateTime parsedDate))
                            {

                                reminder = parsedDate;
                            }
                            else
                            {
                                return "Invaild date format. Skipping reminder.";
                            }
                        }

                        // Add the task to the repository and get the primary key (ID) of the newly created task
                        int taskPK = repo.AddTask(title, description, reminder);

                        // Log the activity of adding a task with the current user's name, the action performed, and details about the task that was created
                        logRepo.AddLogActivity(currentUsername, "ADD TASK", $"Successfully created Task #{taskPK}: {title}");

                        return $"Successfully added!! Your task number is {taskPK} \nYou can use it to:\nView the task\nUpdate the task\nDelete the task";
                        //use taskID so other user's task can't be accessed by other users
                    }
                    catch (FormatException)
                    {
                        return "Invaild format. Use: \n add task, title, description, YYYY/MM/DD 00:00:00 e.g 23:30:08 \n\n ";//provide user with correct format, incase of error
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                        // helps developers debug 
                    }

                }

                if (input.StartsWith("view task"))
                {
                    // Accept either "view task, <id>" or "view task <id>"
                    string cleanUP = input.Substring("view task".Length).Trim();
                    // Remove any leading commas or spaces
                    cleanUP = cleanUP.TrimStart(',', ' ').Trim();

                    if (string.IsNullOrWhiteSpace(cleanUP))// If no task ID is provided after "view task", prompt the user to provide one
                    {
                        return "Please provide a task ID. Use: view task, <taskID> or view task <taskID>";
                    }

                    // Try to parse the task ID and handle potential format issues
                    if (!int.TryParse(cleanUP, out int parsedID))
                    {
                        return "Invalid task ID format. Use: view task, taskID or view task taskID";
                    }

                    var tasks = repo.GetTasks(parsedID);
                    foreach (var task in tasks)
                    {
                        return $"\nTitle: {task.TaskTitle} \nDescription: {task.TaskDescription} \nReminder: {task.TaskReminderDate}\n";
                    }

                    logRepo.AddLogActivity(currentUsername, "VIEW TASK", $"Viwed task {parsedID}");// Log the activity of viewing a task 

                    return $"Task {parsedID} not found.";// If no task is found with the provided ID, inform the user
                    
                }

                // Handle task completion requests
                if (input.StartsWith("completed task"))
                {

                    try
                    {
                        // Accept either "completed task, <id>" or "completed task <id>"
                        string cleanUP = input.Substring("completed task".Length).Trim();
                        cleanUP = input.Replace("completed task", "").TrimStart(',',' '); // Remove the command part and any leading commas or spaces

                        int taskID = Convert.ToInt32(cleanUP);

                        // Mark the task as completed in the repository method
                        repo.CompletedTask(taskID);
                        // Log the activity of marking a task as completed 
                        logRepo.AddLogActivity(currentUsername, "COMPLETED TASK", $"mark task {taskID} as completed");

                        return $"Task {taskID} marked as completed.";// Inform the user that the task has been marked as completed
                    }
                    catch (FormatException)// Handle the case where the task ID is not a valid integer
                    {
                        return "Invalid taskID or taskID does not exist";
                    }

                    catch (Exception ex)
                    {
                        return ex.Message; // general fallback, helps developers debug
                    }
                }

                    // Handle task deletion requests
                if (input.StartsWith("delete task"))
                {
                        try
                        {
                        //Accept either "delete task, <id>" or "delete task <id>"
                        string cleanUP = input.Substring("delete task".Length).Trim();
                        cleanUP = input.Replace("delete task", "").TrimStart(',',' ');

                         int taskID = Convert.ToInt32(cleanUP);

                        // Delete the task from the repository method
                        repo.DeleteTask(taskID);
                        // Log the activity of deleting a task 
                        logRepo.AddLogActivity(currentUsername, "DELETE TASK", $"Deleted task number {taskID}");

                        return $"Task {taskID} has been deleted.";// Inform the user that the task has been deleted
                        }
                        catch
                        {
                            return $"Invaid taskID or taskID does not exist";
                        }

                }



                //this acts a blueprint for the quiz game, it checks if the user is currently taking the quiz and processes their answer accordingly. It also handles the transition between questions and provides feedback on the user's performance.

                if (currentState == ChatState.TakingQuiz)// Check if the chatbot is currently in the quiz-taking state
                {
                    string answer = input.Trim().ToUpper(); // Standardize to A, B, or C

                    if (answer != "A" && answer != "B" && answer != "C")
                    {
                        return "Please answer with either A, B, or C.";
                    }

                    QuizQuestion currentQuestion = quizQuestions[currentQuestionIndex];// Get the current question based on the index, curentQuestion acts as an instance of the QuizQuestion object

                    // if we use return keyword it will exit the method and not allow the next question to be served, so we use a feedback string to store the feedback and return it at the end of the method
                    string feedback = "";

                    // Grade the current question
                    // Check if the user's answer matches the correct answer (case-insensitive)
                    if (answer == currentQuestion.CorrectAnswer.Trim().ToUpper())//.CorrectAnswer this is taken from the QuizQuestion class, which is a blueprint for the quiz questions, and it is used to store the correct answer for each question with getters and setters
                    {
                        quizScore++;
                        feedback = $"✨ Correct!\n💡 Explanation: {currentQuestion.Explanation}\n\n";//.Explanation this is also taken from the QuizQuestion class
                    }
                    else
                    {
                        feedback = $"❌ Incorrect. The correct answer was {currentQuestion.CorrectAnswer}.\n💡 Explanation: {currentQuestion.Explanation}\n\n";
                    }

                    // Move pointer to the next question
                    currentQuestionIndex++;

                    // If there are more questions left, serve the next one
                    if (currentQuestionIndex < quizQuestions.Count)// Check if there are more questions to ask
                    {
                        // Get the next question based on the updated index
                        QuizQuestion nextQuestion = quizQuestions[currentQuestionIndex];//QuizQuestion acts as a object, and nextQuestion as an instance of the object
                        // Prepare the feedback message with the next question and its choices
                        feedback += $"Question {currentQuestionIndex + 1}:\n{nextQuestion.QuestionText}\n" +//.QuestionText this is also taken from the QuizQuestion class
                                    $"A) {nextQuestion.ChoiceA}\n" +//the choices are also taken from the QuizQuestion class 
                                    $"B) {nextQuestion.ChoiceB}\n" +
                                    $"C) {nextQuestion.ChoiceC}";
                        return feedback;
                    }
                    // No questions left so end Game
                    else
                    {
                        currentState = ChatState.Default; // Drop back to regular chat
                        feedback += $"🏆 Quiz Completed!\nYour final score is: {quizScore} / {quizQuestions.Count}\n";// Provide the user with their final score after completing the quiz

                        // Provide feedback based on the user's score                                                                      
                        if (quizScore < 6)
                        {

                            feedback += "Please review the topics above and try the quiz again to improve your score!";
                        }
                        else { 
                        
                            feedback += "Great job! You have a good understanding of cybersecurity awareness. Keep up the good work!";
                        }

                        // Log the activity of taking the quiz with the user's score
                        logRepo.AddLogActivity(currentUsername, "QUIZ ATTEMPT", $"played quiz game score: {quizScore} / {quizQuestions.Count}");

                        return feedback;
                    }
                   

                    
                }


                //this section handles the initiation of the quiz game when the user expresses interest in starting it. It checks for specific keywords in the user's input and sets up the quiz environment accordingly.
                //this part runs first then the above section runs after the user has started the quiz and is answering questions

                if (input.Contains("start quiz") || input.Contains("take quiz") || input.Contains("start game"))
                {
                    // 1. Reads the 'questions.txt' file and loads all 10 questions into memory at once
                    if (quizQuestions.Count == 0)
                    {
                        InitializeQuiz();
                    }

                    // 2. Switches the chatbot state and resets the game tracking numbers
                    currentState = ChatState.TakingQuiz;
                    currentQuestionIndex = 0; // Starts at the beginning (Question 1)
                    quizScore = 0;            // Resets score to zero

                    // 3. Dynamically grabs whatever question is sitting at position 0 (the first line of  file)
                    QuizQuestion firstQuestion = quizQuestions[0];//firstQuestion acts as an instance to load the first question

                    // 4. Returns that first question to the user
                    return $"🎮 Welcome to the Cybersecurity Awareness Quiz! Let's test your skills.\n\n" +
                           $"Question 1:\n{firstQuestion.QuestionText}\n" +
                           $"A) {firstQuestion.ChoiceA}\n" +
                           $"B) {firstQuestion.ChoiceB}\n" +
                           $"C) {firstQuestion.ChoiceC}";

                    //only the first question is returned, the rest of the questions are handled in the above section where the user is answering questions and the chatbot is grading them and providing feedback
                }




                // Handle greetings for non-returning users
                if (HasAnyKeyword(input, "hello", "hi"))
                        return "I told you it is only for returning users, but since you said hi, welcome the topics that I can assist with is above";


                    //Handle gratitude and ending conversation
                    if (HasAnyKeyword(input, "thank you", "thanks"))
                        return "You're welcome! If you have any more questions or need further assistance, feel free to ask 😁👍.";

                    //Catch-all response for unrecognized input error Handling
                    return "I'm sorry, I don't understand. Please rephrase your question or specify a topic you'd like to learn about, such as 'passwords', 'scams', and 'privacy'.";

            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            string userName = txtNameInput.Text.Trim();// Get the user's name from the input field and trim any leading or trailing whitespace

            if (string.IsNullOrWhiteSpace(userName))// Check if the user's name is empty or consists only of whitespace
            {
                ShowFeedback("Error: Name cannot be empty.", Brushes.Red);// If the name is invalid, display an error message in red and clear the input field for the user to try again
                txtNameInput.Clear();// Clear the input field to allow the user to enter a new name
                txtNameInput.Focus();// Set focus back to the input field for user convenience
                return;
            }
            else if (userName.Length < 3)// Check if the user's name is less than 3 characters long
            {
                ShowFeedback("Error: Name must be at least 3 letters long.", Brushes.Red);
                txtNameInput.Clear();
                txtNameInput.Focus();
                return;

            }
            else if (!Regex.IsMatch(userName, @"^[a-zA-Z]+$"))// Check if the user's name contains only letters (no numbers, spaces, or special characters)
            {
                ShowFeedback("Error:  Name must contain letters only.", Brushes.Red);
                txtNameInput.Clear();
                txtNameInput.Focus();
                return;

            }
            else if (userName == "@")// Check if the user's name is the "@" symbol, which is reserved for ending the conversation
            {
                Application.Current.Shutdown();

            }
            else {
                ShowFeedback($"Success!", Brushes.Green);// If the name is valid, display a success message in green and proceed to show the chat interface

                username_grid.Visibility = Visibility.Hidden;// Hide the username input grid to transition to the chat interface

                chats_grid.Visibility = Visibility.Visible;// Show the chat interface grid where the user can interact with the chatbot
                currentUsername = userName;// Set the currentUsername variable to the valid user name for use in personalized interactions and memory functions throughout the chat session
            }
                
        }

        private void ShowFeedback(string message, Brush color)// A helper method to display feedback messages to the user with a specified color (used for both error and success messages)
        {
            txtFeedback.Text = message;// Set the feedback text to the provided message
            txtFeedback.Foreground = color;// Set the feedback text color to the provided color
        }

        static string GetRandomTips(string[] tips) {// A helper method to get a random tip from an array of tips
           
            // Create a new instance of the Random class to generate random numbers
            Random random = new Random();

            // Generate a random index within the bounds of the tips array
            int index = random.Next(tips.Length);

            // Return the tip at the randomly generated index
            return tips[index];
        }

        // A helper method to check if the input string contains any of the specified keywords
        private bool HasAnyKeyword(string input, params string[] keywords) {

            // Iterate through each keyword in the provided keywords array
            foreach (string keyword in keywords) {

                // Check if the input string contains the current keyword (case-insensitive)
                if (input.Contains(keyword)) { 
                    return true;
                }
            
            }
            // If none of the keywords are found in the input string, return false
            return false;
        }

        private void Chatbot_Color(string name, string message)
        {

            //call the chats which is a listview
            ChatArea.Items.Add(
                new TextBlock
                {
                    Inlines = {
                     new Run{
                     Text= name + " : ",
                     Foreground =Brushes.DarkBlue,
                     FontWeight = FontWeights.Bold


                     }   ,
                     new Run {
                     Text= " " + message,
                     Foreground =Brushes.Black

                     }

                    }

                }

                );

        }

        private void User_Color(string name, string message)
        {

            ChatArea.Items.Add(
                new TextBlock
                {
                    Inlines = {
                     new Run{
                     Text= name + " : ",
                     Foreground =Brushes.Purple,
                     FontWeight = FontWeights.Bold


                     }   ,
                     new Run {
                     Text= " " + message,
                     Foreground =Brushes.Black

                     }

                    }

                }

                );


        }

        private void InitializeQuiz()
        {
            quizQuestions = new List<QuizQuestion>();

            // Read every line from file
            string[] lines = File.ReadAllLines("questions.txt");

            foreach (string line in lines)
            {
                // Skip empty lines 
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Split the line by the pipe character '|'
                string[] parts = line.Split('|');

                //helps developers debug to know if the file is broken or missing a pipe symbol
                if (parts.Length < 6)
                {
                    System.Windows.MessageBox.Show($"Error! This line is broken or missing a pipe symbol:\n\n{line}");
                    continue; // Skip this broken line so the app doesn't crash
                }

                // Map the parts directly into QuizQuestion object, like when loaded the tasks
                quizQuestions.Add(new QuizQuestion
                {
                    QuestionText = parts[0],
                    ChoiceA = parts[1],
                    ChoiceB = parts[2],
                    ChoiceC = parts[3],
                    CorrectAnswer = parts[4],
                    Explanation = parts[5]
                });
            }
        }

       
    
    }
}

using System.Diagnostics.Eventing.Reader;
using System.IO.Packaging;
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

namespace Cybersecurity_Awareness_ChatBot_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string[] password =
         {
            "A confidential sequence of characters (letters, numbers, and symbols) used by a user to prove their identity to a system.",
            "A security measure designed to grant authorized users access to a device, application, or network while restricting unauthorized users.",
            "A specific type of shared secret (similar to a passphrase) meant to be known only to the user and the system, often considered something you know in authentication protocols.",
            " A modern watchword or code, often used with a username, to verify that a person is allowed to enter a digital space, similar to a physical key or a password given to a guard",
            "A line of defense designed to protect personal, financial, or confidential information from being stolen by malicious actors (hackers)." };



        string userName = " ";
        string input = " ";
        string reply = " ";
        string currentTopic = " ";
        string currentUsername = " ";

        List<string> sessionTopics = new List<string>();
        bool checkedGreeting = false;

        public MainWindow()
        {
            InitializeComponent();
            PlaySound();
            


            ChatArea.AppendText($"Welcome to the CSABot{currentUsername}, please enter '@' to end our conversation {Environment.NewLine} ");
            ChatArea.AppendText($"I am here to assit you with passwords, scams, and privacy{Environment.NewLine}");
            ChatArea.AppendText($"If you are a returing friend, please greet me with a 'hello' or 'hi' or press send to receive a surprise!!{Environment.NewLine}");//tooo make the chatbot more fun and engaing 
            ChatArea.AppendText($"{Environment.NewLine}");

            

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
            //Create a SoundPlayer instance and set the sound location to the desired audio file
            var soundPlayer = new System.Media.SoundPlayer();
            soundPlayer.SoundLocation = @"C:\Users\Student\Desktop\Recording.wav";
            soundPlayer.PlaySync();
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
            ChatArea.AppendText($"{userName}: {input} {Environment.NewLine}");

            reply = ChatAiResponse(input);// Get the chatbot's response by calling the ChatAiResponse method with the user's input

            ChatArea.AppendText($"Chatbot: {reply}{Environment.NewLine}{Environment.NewLine}");


            // Clear the InputArea TextBox, scroll the ChatScrollViewer to the end to show the latest messages, and set focus back to the InputArea for user convenience
            InputArea.Clear();
            ChatScrollViewer.ScrollToEnd();
            InputArea.Focus();
        }

        
        
        public string ChatAiResponse(string input)
        {
            input = input.Trim().ToLower();

            string[] splitted = input.Split(' ');

            while (true) {

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
                if (input.Contains("password")) detectedTopic = "password";

                if (!string.IsNullOrWhiteSpace(detectedTopic)) { 
                
                    sessionTopics.Add(detectedTopic);// Add the detected topic to the session topics list for potential future reference or analysis

                    // Count how many times the detected topic has been mentioned in the current session
                    int topicCount = 0;
                    foreach(string topic in sessionTopics) { 
                    
                        if (topic == detectedTopic) { 
                            topicCount++;
                        }

                    }

                    //check if they said interested in learning about the topic or asked over 3 times
                    bool isInterested = input.Contains("interested");
                    if (isInterested || topicCount > 3) { 
                    
                        ChatbotMemory.SaveUserInterest(currentUsername, detectedTopic);// Save the user's interest in the detected topic for future reference or personalized interactions
                    }
                }

                // Check for empty input
                if (string.IsNullOrWhiteSpace(input))
                {
                    return "I'm here to assist! Please type a message or ask a question about the topics provided above.";
                }

                //Set current topic 
                if (input.Contains("passwords")) currentTopic = "passwords";

                //Handle sentiemtnal responses
                if (input.Contains("passwords") && input.Contains("frustrated"))
                    return $"I understand that managing passwords can be frustrating, {currentUsername}. Consider using a password manager to securely store and generate strong passwords, which can make it easier to manage multiple accounts without the stress of remembering them all.";

                if (input.Contains("passwords") && input.Contains("worried"))
                    return $"It's normal to feel worried about password security, {currentUsername}. To enhance your security, make sure to use unique passwords for each account, enable two-factor authentication where possible, and regularly update your passwords to reduce the risk of unauthorized access.";

                if (input.Contains("passwords"))
                    return GetRandomTips(password);

                // Handle requests for more information
                if (HasAnyKeyword(input, "explain", "another", "more")) { 
                    
                    if (currentTopic == "passwords") return GetRandomTips(password);

                }

                // Handle greetings for non-returning users
                if (HasAnyKeyword(input, "hello", "hi")) 
                    return "I told you it is only for returning users, but since you said hi, welcome the topics that I can assist with is above";
                

                //Handle gratitude and ending conversation
                if(HasAnyKeyword(input, "thank you", "thanks"))
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

      

       
    }
}
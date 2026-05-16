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
        public MainWindow()
        {
            InitializeComponent();
            PlaySound();
            //CreateColoredText();


            ChatArea.AppendText($"Welcome to the CSABot, please enter '@' to end our conversation {Environment.NewLine} ");
            ChatArea.AppendText($"I am here to assit you with passwords, scams, and privacy{Environment.NewLine}");
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

            input = InputArea.Text.Trim();
            userName = txtNameInput.Text.Trim();


            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            if (input == "@")
            {
                Application.Current.Shutdown();
                return;
            }



            ChatArea.AppendText($"{userName}: {input} {Environment.NewLine}");

            reply = ChatAiResponse(input);

            ChatArea.AppendText($"Chatbot: {reply}{Environment.NewLine}{Environment.NewLine}");

            //CreateColoredText();

            InputArea.Clear();
            ChatScrollViewer.ScrollToEnd();
            InputArea.Focus();
        }

        
        
        public string ChatAiResponse(string input)
        {
            input = input.Trim().ToLower();

            string[] splitted = input.Split(' ');

            while (true) {

                if (input.Contains("password"))
                {
                    currentTopic = "password";
                    return GetRandomTips(password);
                }
                else if (input.Contains("password") && input.Contains("frustrated"))
                {
                    return "I understand that managing passwords can be frustrating. Consider using a password manager to securely store and generate strong passwords, which can make it easier to manage multiple accounts without the stress of remembering them all.";
                }
                else if (input.Contains("password") && input.Contains("worried"))
                {
                    return "It's normal to feel worried about password security. To enhance your security, make sure to use unique passwords for each account, enable two-factor authentication where possible, and regularly update your passwords to reduce the risk of unauthorized access.";
                }
                else if (input.Contains("password") && input.Contains("curious"))
                {
                    return GetRandomTips(password);
                }
                else if (input.Contains("explain") || input.Contains("another") || input.Contains("more"))
                {
                    if (currentTopic == "password")
                    {
                        return GetRandomTips(password);
                    }
                    else if (currentTopic == "phishing")
                    {
                        //return GetRandomTips(phishing);
                    }
                    else
                    {
                        return "Please specify a topic you'd like to learn more about, such as 'password'.";
                    }
                }
                else if (input.Contains("thank you") || input.Contains("thanks"))
                {
                    return "You're welcome! If you have any more questions or need further assistance, feel free to ask.";
                }
                else if (string.IsNullOrWhiteSpace(input))
                {
                    return "I'm here to help! Please type a message or ask a question.";
                }
                else {
                    return "I'm sorry , I don't undersand please rephrase your question";
                }

            }

            
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            
            string userName = txtNameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                ShowFeedback("Error: Name cannot be empty.", Brushes.Red);
                txtNameInput.Clear();
                txtNameInput.Focus();
                return;
            }
            else if (userName.Length < 3)
            {
                ShowFeedback("Error: Name must be at least 3 letters long.", Brushes.Red);
                txtNameInput.Clear();
                txtNameInput.Focus();
                return;

            }
            else if (!Regex.IsMatch(userName, @"^[a-zA-Z]+$"))
            {
                ShowFeedback("Error:  Name must contain letters only.", Brushes.Red);
                txtNameInput.Clear();
                txtNameInput.Focus();
                return;

            }
            else if (userName == "@")
            {
                Application.Current.Shutdown();

            }
            else {
                ShowFeedback($"Success!", Brushes.Green);

                username_grid.Visibility = Visibility.Hidden;

                chats_grid.Visibility = Visibility.Visible;
            }
                
        }

        private void ShowFeedback(string message, Brush color)
        {
            txtFeedback.Text = message;
            txtFeedback.Foreground = color;
        }

        static string GetRandomTips(string[] tips) { 
            Random random = new Random();
            int index = random.Next(tips.Length);
            return tips[index];
        }

      /* private void CreateColoredText() {
            userName = txtNameInput.Text.Trim();
            input = InputArea.Text.Trim();
            reply = ChatAiResponse(input);

            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 16;
            textBlock.Margin = new Thickness(10);

            Run userRun = new Run($"{userName}: ");
            userRun.FontWeight= FontWeights.Bold;
            userRun.Foreground = Brushes.Indigo;

            Run messageRun = new Run(input);
            messageRun.Foreground = Brushes.Black;

            Run botRun = new Run("CSABot: ");
            botRun.FontWeight = FontWeights.Bold;
            botRun.Foreground = Brushes.DarkBlue;

            Run responseRun = new Run(reply);
            responseRun.Foreground = Brushes.Black;

            textBlock.Inlines.Add(userRun);
            textBlock.Inlines.Add(messageRun);
            textBlock.Inlines.Add(botRun);
            textBlock.Inlines.Add(responseRun);

            ChatStackPanel.Children.Add(textBlock);
            ChatScrollViewer.ScrollToEnd();
        }*/

       
    }
}
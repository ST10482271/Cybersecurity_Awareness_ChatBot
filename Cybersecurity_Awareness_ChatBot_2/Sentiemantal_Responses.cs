using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Awareness_ChatBot_2
{
    public class Sentiemantal_Responses
    {
        //sentiemantal response arrays
        string[] passwordFrustrated = 
        {
          "I understand that managing passwords can be frustrating, {currentUsername}. Consider using a password manager to securely store and generate strong passwords, which can make it easier to manage multiple accounts without the stress of remembering them all.",
          "It sounds like you're feeling overwhelmed with passwords, {currentUsername}. One tip is to create a memorable passphrase by combining random words, which can be easier to remember and still provide strong security.",
          "I can see that passwords are causing you some frustration, {currentUsername}. A good practice is to use two-factor authentication (2FA) whenever possible, which adds an extra layer of security and can help reduce the stress of worrying about password breaches."
        };

        public string[] passwordsWorried = 
        {
          "I understand that worrying about passwords can be stressful, {currentUsername}. Consider using a password manager to securely store and generate strong passwords, which can help reduce anxiety about remembering them all.",
          "It sounds like you're feeling concerned about password security, {currentUsername}. One tip is to enable two-factor authentication (2FA) whenever possible, which adds an extra layer of protection and can help ease your worries.",
          "I can see that password security is causing you some concern, {currentUsername}. A good practice is to regularly update your passwords and avoid reusing them across multiple accounts, which can help mitigate potential risks."
        };

        public string[] scamsFearful = 
        {
          "I understand that scams can be frightening, {currentUsername}. Always be cautious of unsolicited messages and never share personal information or financial details with unknown sources.",
          "It sounds like you're feeling fearful about scams, {currentUsername}. A good tip is to verify the legitimacy of any communication by contacting the company or individual directly using official contact information.",
          "I can see that scams are causing you some fear, {currentUsername}. Remember to keep your software and security systems up to date, as this can help protect you from potential threats."
        };

        public string[] scamsCurious = 
        {
          "It's great that you're curious about scams, {currentUsername}. Learning about common scam tactics can help you recognize and avoid them. Always be skeptical of offers that seem too good to be true.",
          "Your curiosity about scams is a positive step towards staying informed, {currentUsername}. Consider researching common types of scams, such as phishing or social engineering, to better understand how to protect yourself.",
          "Being curious about scams can lead to greater awareness, {currentUsername}. A useful tip is to educate yourself on how scammers operate and to share this knowledge with friends and family to help keep everyone safe."
        };

        public string[] scamsFrustrated = {

            "I understand that dealing with scams can be frustrating, {currentUsername}. Remember to stay vigilant and report any suspicious activity to the appropriate authorities to help protect yourself and others.",
            "It sounds like you're feeling frustrated about scams, {currentUsername}. A good tip is to regularly review your financial statements and credit reports for any unauthorized activity, which can help you catch potential scams early.",
            "I can see that scams are causing you some frustration, {currentUsername}. A good practice is to educate yourself on common scam tactics and to share this knowledge with friends and family, which can help reduce the overall impact of scams in your community."

        };

        public string[] privacyWorried = 
        {
          "I understand that privacy concerns can be unsettling, {currentUsername}. Consider using strong, unique passwords and enabling two-factor authentication to help protect your accounts and personal information.",
          "It sounds like you're feeling worried about privacy, {currentUsername}. A good tip is to regularly review the privacy settings on your social media accounts and limit the amount of personal information you share online.",
          "I can see that privacy issues are causing you some worry, {currentUsername}. Remember to be cautious about the apps and websites you use, and only provide necessary information to trusted sources."
        };

        public string[] privacyCurious = 
        {
          "It's great that you're curious about privacy, {currentUsername}. Learning about how to protect your personal information online can help you make informed decisions about what to share and with whom.",
          "Your curiosity about privacy is a positive step towards staying informed, {currentUsername}. Consider researching best practices for online privacy, such as using a virtual private network (VPN) or adjusting your browser settings to enhance your security.",
          "Being curious about privacy can lead to greater awareness, {currentUsername}. A useful tip is to regularly check the permissions of the apps you use and to be mindful of the information you share on social media platforms."
        };

        public string[] phishingFearful = 
        {
          "I understand that phishing can be scary, {currentUsername}. Always be cautious of unsolicited messages and never click on links or download attachments from unknown sources.",
          "It sounds like you're feeling fearful about phishing, {currentUsername}. A good tip is to verify the legitimacy of any communication by contacting the company or individual directly using official contact information.",
          "I can see that phishing is causing you some fear, {currentUsername}. Remember to keep your software and security systems up to date, as this can help protect you from potential threats."
        };

        public string[] phishingCurious =
        {
            "It's great that you're curious about phishing, {currentUsername}. Learning about common phishing tactics can help you recognize and avoid them. Always be skeptical of offers that seem too good to be true.",
            "Your curiosity about phishing is a positive step towards staying informed, {currentUsername}. Consider researching common types of phishing attacks, such as spear phishing or whaling, to better understand how to protect yourself.",
            "Being curious about phishing can lead to greater awareness, {currentUsername}. A useful tip is to educate yourself on how phishers operate and to share this knowledge with friends and family to help keep everyone safe."
        };

        
          
        //method to detect and respond 
        public string GetSentimentalResponse(string input, string currentUsername)
        {
            string lowerInput = input.ToLower(); // Convert input to lowercase for case-insensitive comparison
            if (lowerInput.Contains("passwords")){ 
              
                if (lowerInput.Contains("worried"))
                    return GetRandomTips(passwordsWorried).Replace("{currentUsername}", currentUsername);

                if (lowerInput.Contains("frustrated"))
                    return GetRandomTips(passwordFrustrated).Replace("{currentUsername}", currentUsername);

            }

            if(lowerInput.Contains("scams"))
            {
                if (lowerInput.Contains("fearful"))
                    return GetRandomTips(scamsFearful).Replace("{currentUsername}", currentUsername);

                if (lowerInput.Contains("curious"))
                    return GetRandomTips(scamsCurious).Replace("{currentUsername}", currentUsername);

                if (lowerInput.Contains("frustrated"))
                    return GetRandomTips(scamsFrustrated).Replace("{currentUsername}", currentUsername);
            }

            if (lowerInput.Contains("privacy"))
            {
                if (lowerInput.Contains("worried"))
                    return GetRandomTips(privacyWorried).Replace("{currentUsername}", currentUsername);

                if (lowerInput.Contains("curious"))
                    return GetRandomTips(privacyCurious).Replace("{currentUsername}", currentUsername);

               
            }

            if(lowerInput.Contains("phishing"))
            {
                 if (lowerInput.Contains("fearful"))
                    return GetRandomTips(phishingFearful).Replace("{currentUsername}", currentUsername);

                if (lowerInput.Contains("curious"))
                    return GetRandomTips(phishingCurious).Replace("{currentUsername}", currentUsername);
            }

            return string.Empty;
        }

        static string GetRandomTips(string[] tips)
        {// A helper method to get a random tip from an array of tips

            // Create a new instance of the Random class to generate random numbers
            Random random = new Random();

            // Generate a random index within the bounds of the tips array
            int index = random.Next(tips.Length);

            // Return the tip at the randomly generated index
            return tips[index];
        }
    }  

}

 

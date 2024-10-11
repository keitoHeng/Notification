using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NotificationParser
{
    class Program
    {
        // notification channels
        private static readonly HashSet<string> notificationChannels = new HashSet<string>
        {
            "BE", "FE", "QA", "Urgent"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Enter notification titles (type 'exit' to quit):");

            while (true)
            {
                string input = Console.ReadLine();
                if (input?.ToLower() == "exit") break; 

                List<string> channels = ParseNotificationChannels(input);
                Console.WriteLine($"Input: \"{input}\"");

                // Display different messages based on detected channels
                if (channels.Count > 0)
                {
                    Console.WriteLine("Receive channels: " + string.Join(", ", channels));

                    // Display a message based on channels
                    if (channels.Contains("Urgent"))
                    {
                        Console.WriteLine("Action Required: Urgent notification!");
                    }
                    if (channels.Contains("BE"))
                    {
                        Console.WriteLine("Backend team needs to be alerted.");
                    }
                    if (channels.Contains("FE"))
                    {
                        Console.WriteLine("Frontend team should review.");
                    }
                    if (channels.Contains("QA"))
                    {
                        Console.WriteLine("Quality Assurance team should test.");
                    }
                }
                else
                {
                    Console.WriteLine("Receive channels: None");
                }
                Console.WriteLine();
            }
        }

        // parse notification channels from a title
        private static List<string> ParseNotificationChannels(string title)
        {
            var matches = Regex.Matches(title, @"\[(.*?)\]");
            List<string> channels = new List<string>();

            foreach (Match match in matches)
            {
                string tag = match.Groups[1].Value.Trim();
                if (notificationChannels.Contains(tag))
                {
                    channels.Add(tag);
                }
            }

            return channels;
        }
    }
}
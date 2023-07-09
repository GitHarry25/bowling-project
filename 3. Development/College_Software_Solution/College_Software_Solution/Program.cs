using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO;


namespace CollegeScoringSystem
{
    class Program
    {
        static void Main(string[] args)
        {
       
            Console.Title = "College Software Solution"; // Title of the application
            const int iNumEvents = 5; // Number of events
            const int iNumTeams = 4; // Number of teams
            int[,] iScores = new int[iNumEvents, iNumTeams]; 
            string[,] sTeams = new string[iNumTeams, 6]; // Array to store team names and players
            int[] sTeamScores = new int[iNumTeams]; // Array to store team scores
            string playerName; // String to store player name
            
            

            // Menu screen
            DialogResult result = MessageBox.Show("Hello, would you like to add a team?", "Menu Screen", MessageBoxButtons.YesNo); 
            if (result == DialogResult.Yes)
            {
                // Continues with the program
                
            }
            else
            {
                Environment.Exit(0); // Closes the program
            }


            // Collect team names and player names
            for (int i = 0; i < iNumTeams; i++)
            {
                
                Console.ForegroundColor = ConsoleColor.Green; // Adding green colour
                Console.Write("Enter team " + (i + 1) + " name: ");
                Console.ForegroundColor = ConsoleColor.White; // Reverting to original color
                sTeams[i, 0] = Console.ReadLine();

                // Validate team name input
                while (string.IsNullOrWhiteSpace(sTeams[i, 0]))
                    {
                        Console.Write("Invalid input! Team name cannot be empty. Enter team " + (i+1) + " name again: ");
                        sTeams[i, 0] = Console.ReadLine();
                    }

                // Collect player names for the team
                for (int j = 1; j <= 5; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Adding green colour
                    Console.Write("Enter player " + j + " name: ");
                    Console.ForegroundColor = ConsoleColor.White; // Reverting to original color
                    playerName = Console.ReadLine(); 

                    // Validate player name input
                    while (string.IsNullOrWhiteSpace(playerName))
                    {
                        Console.Write("Invalid input! Player name cannot be empty. Enter player " + j + " name again: ");
                        playerName = Console.ReadLine();
                    }

                    sTeams[i, j] = playerName; // Assigning the name to the 2D array

                }
                Console.Clear(); // Clearing the interface
            }

            // Collect scores for each event and each team
            for (int i = 0; i < iNumEvents; i++)
            {
                Console.WriteLine("Enter scores for event " + (i + 1));

                for (int j = 0; j < iNumTeams; j++)
                {
                    Console.Write("Enter score for " + sTeams[j, 0] + ": ");
                    int score = 0;

                    // Validate score input
                    while (!int.TryParse(Console.ReadLine(), out score) || score < 0 || score > 10)
                    {
                        Console.Write("Invalid input! Enter a score between 0-10: ");
                    }

                    iScores[i, j] = score;
                    sTeamScores[j] += score; // Add the score to the team's total score
                    
                }
                Console.Clear(); // Clear the interface
            }

            // Sort teams by total score in descending order
            int[] teamRanking = Enumerable.Range(0, iNumTeams).ToArray();
            Array.Sort(teamRanking, (a, b) => -sTeamScores[a].CompareTo(sTeamScores[b]));

            // Display scores for each event and each team
            Console.WriteLine("\nScores for each event and team:");
            for (int i = 0; i < iNumEvents; i++)
            {
                Console.WriteLine("\nEvent " + (i + 1) + ":"); // Event number
                for (int j = 0; j < iNumTeams; j++) // Loop for each event's scores
                {
                    Console.Write(sTeams[j, 0] + ": " + iScores[i, j] + " ("); 
                    for (int k = 1; k <= 5; k++)
                    {
                        Console.Write(sTeams[j, k] + ", ");
                    }
                    Console.WriteLine(")");
                }
            }
            Thread.Sleep(10000); // 10 second delay
            Console.Clear(); // Clear the interface

            // Display team rankings by total score in descending order
            Console.WriteLine("\nTeam Rankings:");
            for (int i = 0; i < iNumTeams; i++)
            {
                Console.WriteLine((i + 1) + ". " + sTeams[teamRanking[i], 0] + ": " + sTeamScores[teamRanking[i]]);

            }
            
            // Record the data to a file
            using (StreamWriter sw = new StreamWriter("Scoreboard.txt")) // Name of text document
            {
                // Create the heading disiplay
                sw.WriteLine("Tournament scores");
                for (int i = 0; i < iNumTeams; i++) 
                {
                    sw.WriteLine(sTeams[i, 0] + ": " + sTeamScores[i]); // This writes the name and scores of each team to a text document
                }
            }

            // End screen
            Thread.Sleep(5000); // 5 second delay
            DialogResult result2 = MessageBox.Show("Thanks for playing. Would you like to go again?", "Menu Screen", MessageBoxButtons.YesNo);
            if (result2 == DialogResult.Yes)
            {
                Application.Restart();
            }
            else
            {
                Environment.Exit(0); // Closes the program
            }
            Console.ReadLine();
        }
    }
}





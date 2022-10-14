using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace U2_19
{
    /// <summary>
    /// For reading / writing data
    /// </summary>
    static class InOutUtils
    {
        /// <summary>
        /// from reading data from file
        /// </summary>
        /// <param name="fileName"> name of file </param>
        /// <param name="Team1"> returns first team </param>
        /// <param name="Team2"> returns second team </param>
        /// <returns> register of players</returns>
        public static PlayersRegister ReadPlayers(string fileName, out string Team1, out string Team2)
        {
            Team1 = "";
            Team2 = "";
            PlayersRegister register = new PlayersRegister();
            string[] Lines = File.ReadAllLines(fileName);
            register.Cycle = int.Parse(Lines[0]);
            register.CycleDate = DateTime.Parse(Lines[1]);

            foreach (string line in Lines.Skip(2))
            {
                string[] Values = line.Split(';');
                string name = Values[0];
                string lastName = Values[1];
                string team = Values[2];
                if (Team1 == "")
                {
                    Team1 = team;
                }
                else if (Team1 != team && Team2 == "")
                {
                    Team2 = team;
                }               
                Position position;
                Enum.TryParse(Values[3], out position);

                string champion = Values[4];
                int kills = int.Parse(Values[5]);
                int assists = int.Parse(Values[6]);
                Player player = new Player(name, lastName, team, position, champion, kills, assists);
                register.AddPlayer(player);
            }
            return register;
        }

        /// <summary>
        /// prints out team with most player assists
        /// </summary>
        /// <param name="players"> register of players </param>
        /// <param name="Team1"> the first team </param>
        /// <param name="Team2"> the second team </param>
        public static void PrintBestTeam(PlayersRegister players, string Team1, string Team2)
        {
            int team1 = players.FindTeamAssists(Team1);
            int team2 = players.FindTeamAssists(Team2);
            if (team1 > team2)
            {
                Console.WriteLine("|{0,6}|{1,-10}|", players.Cycle, Team1);
            }
            else if (team2 > team1)
            {
                Console.WriteLine("|{0,6}|{1,-10}|", players.Cycle, Team2);
            }
            else
            {
                Console.WriteLine("|{0,6}|{1,-10}|", players.Cycle, Team1);
                Console.WriteLine("|{0,6}|{1,-10}|", players.Cycle, Team2);
            }

        }
        /// <summary>
        /// prints out best teams
        /// </summary>
        /// <param name="r1"> first register </param>
        /// <param name="r2"> second register </param>
        /// <param name="T1C1"> first team in first cycle </param>
        /// <param name="T2C1"> second team in first cycle </param>
        /// <param name="T1C2"> first team in second cycle </param>
        /// <param name="T2C2"> second team in second cycle </param>
        public static void PrintBestTeams(PlayersRegister r1, PlayersRegister r2, string T1C1, string T2C1, string T1C2, string T2C2)
        {
            Console.WriteLine(new string('-', 19));
            Console.WriteLine("|{0, 6}|{1, -10}|", "Ciklas", "Komanda");
            Console.WriteLine(new string('-', 19));
            PrintBestTeam(r1, T1C1, T2C1);
            PrintBestTeam(r2, T1C2, T2C2);
            Console.WriteLine(new string('-', 19));
        }

        /// <summary>
        /// prints out best player
        /// </summary>
        /// <param name="r3"> the third register </param>
        public static void PrintBestPlayer(PlayersRegister r3)
        {
            int count = r3.FindBiggestKA();
            Console.WriteLine(new String('-', 56));
            Console.WriteLine("|{0, -10}|{1, -10}|{2, -10}|{3, -10}|{4, -10}|", "Vardas", "Pavardė", "Komanda", "Pozicija", "Čempionas");
            Console.WriteLine(new String('-', 56));
            for (int i = 0; i < r3.PlayersCount(); i++)
            {
                if (r3.GetKA(i) == count)
                {
                    Player player = r3.GetByIndex(i);
                    Console.WriteLine("|{0,-10}|{1,-10}|{2,-10}|{3,-10}|{4,-10}|", player.Name, player.LastName, player.Team, player.Position, player.Champion);
                }
            }
            Console.WriteLine(new String('-', 56));
        }

        /// <summary>
        /// prints out all champions
        /// </summary>
        /// <param name="champions"> list of champions </param>
        /// <param name="fileName"> name of file </param>
        public static void PrintChampions(List<string> champions, string fileName)
        {
            string[] lines = new string[champions.Count];

            for (int i = 0; i < champions.Count; i++)
            {
                lines[i] = champions[i] + ';';
            }
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

        /// <summary>
        /// prints given register 
        /// </summary>
        /// <param name="players"> register of players </param>
        public static void PrintTXT(PlayersRegister players)
        {

            string[] Lines = new string[players.PlayersCount() + 12];
            Lines[0] = String.Format("Rato numeris: {0}", players.Cycle);
            Lines[1] = String.Format("Data: {0:yyyy-MM-dd}", players.CycleDate);
            Lines[2] = String.Format("");
            Lines[3] = String.Format(new string('-', 87));
            Lines[4] = String.Format("|{0, -15}|{1, -15}|{2, -15}|{3, -10}|{4, -15}|{5, -5}|{6, -5}|", "Vardas", "Pavardė", "Komanda", "Pozicija", "Čempionas", "K", "A");
            Lines[5] = String.Format(new string('-', 87));
            for (int i = 0; i < players.PlayersCount(); i++)
            {
                Player player = players.GetByIndex(i);
                Lines[i + 6] = String.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-10}|{4,-15}|{5,-5}|{6,-5}|", player.Name, player.LastName, player.Team, player.Position, player.Champion, player.Kills, player.Assists);
            }
            Lines[players.PlayersCount() + 6] = String.Format(new string('-', 87));

            File.AppendAllLines("Žaidėjai.txt", Lines);
        }

        /// <summary>
        /// prints both registers
        /// </summary>
        /// <param name="r1"> first register </param>
        /// <param name="r2"> second register </param>
        public static void PrintAllToTXT(PlayersRegister r1, PlayersRegister r2)
        {
            if (File.Exists("Žaidėjai.txt"))
            {
                File.Delete("Žaidėjai.txt");
            }
            PrintTXT(r1);
            PrintTXT(r2);

        }

    }
}

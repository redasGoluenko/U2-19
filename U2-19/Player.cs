using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_19
{                                                             
    /// <summary>
    /// for storing information of player
    /// </summary>
    class Player
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Team { get; set; }
        public Position Position { get; set; }
        public string Champion { get; set; }
        public int Kills { get; set; }
        public int Assists { get; set; }



        /// <summary>
        /// construction for player class
        /// </summary>
        /// <param name="name"> name of player </param>
        /// <param name="lastName"> surname of player </param>
        /// <param name="team"> team of player</param>
        /// <param name="position"> position of player <param>
        /// <param name="champion"> champion of player </param>
        /// <param name="kills"> kills of player </param>
        /// <param name="assists"> assists of player </param>
        public Player(string name, string lastName, string team, Position position, string champion, int kills, int assists)
        {
            this.Name = name;
            this.LastName = lastName;
            this.Team = team;
            this.Position = position;
            this.Champion = champion;
            this.Kills = kills;
            this.Assists = assists;
        }
        /// <summary>
        /// Hash code override is an essential tool that makes sure nothing prevents class from functioning properly in conjunction with hash based collections
        /// </summary>
        
        public override bool Equals(object obj)
        {
            return obj is Player player &&
                   Name == player.Name &&
                   LastName == player.LastName &&
                   Team == player.Team &&
                   Champion == player.Champion;
        }

        public override int GetHashCode()
        {
            int hashCode = 1288566522;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Team);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Champion);
            return hashCode;
        }
    }
}

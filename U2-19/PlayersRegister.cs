using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace U2_19
{
    /// <summary>
    /// Register of players
    /// </summary>
    class PlayersRegister
    {
        public int Cycle { get; set; }
        public DateTime CycleDate { get; set; }

        private List<Player> AllPlayers = new List<Player>();

        /// <summary>
        /// adds player to private list if players isnt already inside
        /// </summary>
        /// <param name="player"> player </param>
        /// <returns> private list used to store information using register methods </returns>
        public List<Player> AddPlayer(Player player)
        {
            if (!AllPlayers.Contains(player))
            {
                this.AllPlayers.Add(player);
            }
            return AllPlayers;
        }

        /// <summary>
        /// finds amount of players in private list
        /// </summary>
        /// <returns> count of players in private list </returns>
        public int PlayersCount()
        {
            return AllPlayers.Count;
        }

        /// <summary>
        /// finds player by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Player GetByIndex(int index)
        {
            return this.AllPlayers[index];
        }

        /// <summary>
        /// finds assists of specified team
        /// </summary>
        /// <param name="team"> paramter used to give team </param>
        /// <returns> count of assists in specified team </returns>
        public int FindTeamAssists(string team)
        {
            int count = 0;
            for(int i = 0; i < PlayersCount(); i++)
            {
                Player player = this.GetByIndex(i);
                if(player.Team.Equals(team))
                {
                    count += player.Assists;
                }
            }
            return count;
        }     

        /// <summary>
        /// finds count of biggest kill assist sum
        /// </summary>
        /// <returns> count of biggest kill assist sum </returns>
        public int  FindBiggestKA()
        {
            int count = 0;
            for(int i = 0; i < PlayersCount(); i++)
            {
                Player player = this.GetByIndex(i);
                if (player.Kills + player.Assists > count)
                {
                    count = player.Kills + player.Assists;
                }              
            }
            return count;
        }    

        /// <summary>
        /// merges both registers into one
        /// </summary>
        /// <param name="register"> register to merge with</param>
        /// <returns> merged register </returns>
        public PlayersRegister Merge(PlayersRegister register)
        {
            PlayersRegister r3 = new PlayersRegister();
            for(int i = 0; i < this.PlayersCount(); i++)
            {
               r3.AddPlayer(register.GetByIndex(i));
                r3.AddPlayer(this.GetByIndex(i));
            }
            return r3;
        }

        /// <summary>
        /// gets sum of kills and assists of player with specified index
        /// </summary>
        /// <param name="index"> index of desired player </param>
        /// <returns> sum of players kills and assists </returns>
        public int GetKA(int index)
        {
            return this.AllPlayers[index].Assists + this.AllPlayers[index].Kills;
        }

        /// <summary>
        /// finds every champion present in both registers
        /// </summary>
        /// <returns> list of champions </returns>
        public List<string> FindChampions()
        {
            List<string> Champions = new List<string>();
            for (int i = 0; i < PlayersCount(); i++)
            {
                Player player = this.GetByIndex(i);
                string champion = player.Champion;
                if (!Champions.Contains(champion))
                {
                    Champions.Add(champion);
                }
            }
            return Champions;
        }
    }
}

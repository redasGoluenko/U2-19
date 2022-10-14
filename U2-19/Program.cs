using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_19
{
    /// <summary>
    /// main function
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {        
            Console.InputEncoding = Encoding.GetEncoding(1257);
            Console.OutputEncoding = Encoding.UTF8;
            string Team1C1, Team1C2, Team2C1, Team2C2;
            Console.WriteLine("Kiekviename rate geriausiai bendradarbiavusios komandos:");
            PlayersRegister register1 = InOutUtils.ReadPlayers("Dalyviai1.csv", out Team1C1, out Team2C1);
            PlayersRegister register2 = InOutUtils.ReadPlayers("Dalyviai2.csv",out Team1C2, out Team2C2);
            PlayersRegister register3 = register1.Merge(register2);
            InOutUtils.PrintBestTeams(register1,register2, Team1C1, Team2C1, Team1C2, Team2C2);
            Console.WriteLine();        
            Console.WriteLine("Žaidėjas,pademonstravęs geriausią bendrą (per abu ratus) asmeninį rezultatą:");
            InOutUtils.PrintBestPlayer(register3);
            string fileName = "Champions.csv";
            InOutUtils.PrintChampions(register3.FindChampions(), fileName);
            InOutUtils.PrintAllToTXT(register1, register2);      
            InOutUtils.PrintAllToTXT(register1, register2);

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Simulation
{
    internal class Katasztrofa
    {


        public string nev { get; private set; }

        public int aldozatok { get; private set; }
        public int eselyesevszak { get; private set; }

        public string kategoria { get; private set; }   

        public Katasztrofa(string Nev, int Eselyesevszak, int maximumHalalesetek) {
            nev = Nev;
            if (Eselyesevszak > 5 || Eselyesevszak < 1)
            {
                throw new Exception($"Az Eselyesevszak változó csak 1 és 5 között vár értéket!\n (1-4 évszakok, 5: minden évszak)  ");
            }
            else
            {
                eselyesevszak = Eselyesevszak;
            }

            kategoria = randomKategoria();
            aldozatok = randomAldozat(maximumHalalesetek);
        }


        public int randomAldozat(int maximum)
        {
            if (nev == "Nem volt" || kategoria == "gyenge")
            {
                return 0;
            }
 
            Random random = new Random();      
            int aldozat = random.Next(0,maximum);
            return aldozat;
        }

        public string randomKategoria()
        {
            if (nev == "Nem volt")
            {
                return "";
            }

            Random random = new Random();
            string Kategoria = "";
            int suly = random.Next(780); //// 5, 25, 125, 625

            if(suly < 5)
            {
                Kategoria = "nagyon súlyos";
            }
            else if(suly > 4 && suly < 24)
            {
                Kategoria = "súlyos";
            }
            else if(suly > 23 && suly < 124)
            {
                Kategoria = "közepes";
            }
            else if(suly > 123 && suly < 625)
            {
                Kategoria = "gyenge";
            }


            return Kategoria;
        }

        public override string ToString()
        {
            string elvalaszto = "──────────────────────────────────────────────────────";
            if (nev == "Nem volt")
            {
        
                return $"\n{elvalaszto}\n    Katasztrófa: \tNem dokumentáltak katasztrófát";
            }
            return $"\n{elvalaszto}\n     Katasztrófa:\t\t\t{nev} \n    \t    Súly: \t\t\t{kategoria}\n Áldozatokszáma: \t\t\t{aldozatok}";
        }
    }
}

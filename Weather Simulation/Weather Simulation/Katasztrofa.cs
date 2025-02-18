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
        public double Esely { get; set; }
        private List<string> SpecialChars = new List<string>(){
        "~",
        "!",
        "@",
        "#",
        "$",
        "%",
        "^",
        "&",
        "*",
        "_",
        "-",
        "+",
        "=",
        "`",
        "|",
        "(",
        ")",
        "{",
        "}",
        "[",
        "]",
        ":",
        ";",
        "'",
        "<",
        ">",
        ",",
        ".",
        "?",
        "/",
        };
        public Katasztrofa(string Nev, int Eselyesevszak, int maximumHalalesetek, double esely)
        {
            for (int i = 0; i < Nev.Length; i++)
            {
                if (SpecialChars.Contains(Convert.ToString(Nev[i])))
                {
                    throw new Exception($"\n   A katasztrófa neve nem tartalmazhat speciális karaktereket!");
                }
            }
                if (nev == "")
                {
                    throw new Exception($"\n   A név mező nem lehet üres!");
                }

            
                else
                {
                    nev = Nev;

                }
                if (Eselyesevszak > 5 || Eselyesevszak < 1)
                {
                    throw new Exception($"\n   Az Eselyesevszak változó csak 1 és 5 között vár értéket!\n   A megadott érték: {Eselyesevszak}");
                }
                if (Convert.ToString(Eselyesevszak) == "")
                {
                    throw new Exception($"\n   Az esélyes évszak mező nem lehet üres!");
                }
                else
                {
                    eselyesevszak = Eselyesevszak;
                }
                if (esely < 0)
                {
                    throw new Exception("\n   Egy katasztrófa történésének esélye nem lehet 0-nál kevesebb %!");
                }
                if (esely > 100)
                {
                    throw new Exception("\n   Egy katasztrófa történésének esélye nem lehet 100-nál több %!");
                }
                else
                {
                    Esely = esely;

                }
                if (maximumHalalesetek < 0)
                {
                    throw new Exception("\n   A maximum halálesetek száma nem lehet kevesebb, mint 0!");
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
            return $"\n{elvalaszto}\n     Katasztrófa:\t\t\t{nev} \n    \t    Súly: \t\t\t{kategoria}\n  Áldozatokszáma: \t\t\t{aldozatok}";
        }
    }
}

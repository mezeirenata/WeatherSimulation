using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Simulation
{

    internal class Csapadek
    {

        public double mennyiseg { get; private set; }
        public string Csapadekforma { get; private set; }
        public bool Hoformaju { get;private set; }

        private string[] SpecialChars = new string[] {
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

        private int sorszam { get; set; }
        public Csapadek(string Forma, bool _Hoformaju, int Sorszam, double Mennyiseg) {
            Csapadekforma = CsapadekFeltoltese(Forma);
            mennyiseg = Mennyiseg;
            sorszam = Sorszam;
            Hoformaju = _Hoformaju;
        }

        public string CsapadekFeltoltese(string forma)
        {

            if (egyebKarakter(forma))
            {
                throw new Exception("Helytelen csapadékforma!");
            }
            else
            {
                return forma;
            }

        }
        public void _sorszam(int Sorszam)
        {
            sorszam = Sorszam;

        }

        public double randomMennyiseg(int minMennyiseg,int maxMennyiseg)
        {
            if (maxMennyiseg == 0)
            {
                mennyiseg = 0.0;
            }
            else
            {
                Random random = new Random();
                if (Hoformaju)
                {
                    mennyiseg = random.Next(minMennyiseg, maxMennyiseg + 1) / 10.0;
                }
                else
                {
                    if (Csapadekforma == "Ónos eső")

                    mennyiseg = (random.Next(minMennyiseg, maxMennyiseg + 1) + 50) / 100.0;
                }
            }
              return mennyiseg;

        }

        private bool egyebKarakter(string forma)
        {
            for(int i = 0; i < SpecialChars.Length;i++ )
            if (forma.Contains(SpecialChars[i]))
            {
                return true;
            }
            return false;
        }


        public override string ToString()
        {
            string csapadeknev = Csapadekforma;
            if (mennyiseg  == 0.0)
            {
                csapadeknev = "Csapadék";
            }
            return $"\n  \t{csapadeknev} :\t\t\t{this.mennyiseg} mm";
        }
    }
}

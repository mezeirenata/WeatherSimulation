using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Simulation
{

    internal class Csapadek
    {

        private double mennyiseg { get; set; }
        public string Csapadekforma { get; private set; }

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
        public Csapadek(string Forma) {
            Csapadekforma = CsapadekFeltoltese(Forma);
            mennyiseg = 0.0;
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

        public double randomMennyiseg(int minMennyiseg,int maxMennyiseg)
        {
            if (maxMennyiseg == 0)
            {
                mennyiseg = 0.0;
            }
            else
            {
                Random random = new Random();
                mennyiseg = random.Next(minMennyiseg,maxMennyiseg+1) / 100.0;

            }
            if (Csapadekforma.Contains("hó") || Csapadekforma.Contains("havazás"))
            {
                mennyiseg /= 10.0;
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
    
    
    
    }
}

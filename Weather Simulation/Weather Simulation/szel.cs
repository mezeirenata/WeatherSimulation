using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Simulation
{
    internal class Szel
    {
        public string szelirany { get; private set; }

        public double szelsebesseg { get; private set; }
        private int legvaloszinubbSzam { get; set; }
        private int legvaloszinubbSzam2 { get; set; }
        public List<string> Szeliranyok { get;private set; } = new List<string>()
        {
            "Nyugati",
            "Északnyugati",
            "Északi",
            "Északkeleti",
            "Keleti",
            "Délkeleti",
            "Déli",
            "Délnyugati"
        };
        

        public Szel(int legvaloszinubbszam, int legvaloszinubbszam2)
        {
            szelirany = SzeliranyBeallitasa();
            legvaloszinubbSzam = legvaloszinubbszam;
            legvaloszinubbSzam2 = legvaloszinubbszam2;
            szelsebesseg = randomSebesseg();
        }

        private string SzeliranyBeallitasa()
        {

            Random random = new Random();
            int szam = random.Next(0, 5); /// 5 db szám 0-4
            if (szam < 4)
            {
                szelirany = Szeliranyok[legvaloszinubbSzam];
            }
            else
            {
                int randomirany = random.Next(0, Szeliranyok.Count);
                szelirany = Szeliranyok[randomirany];
            }
            return szelirany;
             
        }

        private double randomSebesseg()
        {
            Random random = new Random();

            if (legvaloszinubbSzam2 == 0)
            {
                szelsebesseg = random.Next(700, 1100);

            }
            else if(legvaloszinubbSzam2 < 11 && legvaloszinubbSzam2 > 0)
            {
                szelsebesseg = random.Next(400, 600); 
            }
            else if (   legvaloszinubbSzam2 > 10 && legvaloszinubbSzam2 < 50)
            {
                szelsebesseg = random.Next(250,399);
            }
            else if  (legvaloszinubbSzam2 > 49 && legvaloszinubbSzam2 < 76)
            {
                szelsebesseg = random.Next(130,200);
            }
            else if (legvaloszinubbSzam2 > 75 &&    legvaloszinubbSzam2 < 92)
            {
                szelsebesseg = random.Next(75,110);
            }
            else if (legvaloszinubbSzam2 > 91 && legvaloszinubbSzam2 < 100)
            {
                szelsebesseg = random.Next(10,50);
            }

            return szelsebesseg / 10.0;

        }

        public override string ToString()
        {
            return $"\tÁtlagos Szélsebesség:{this.szelsebesseg} km/h \n \t\tSzélirány: {this.szelirany}";
        }
    }
}

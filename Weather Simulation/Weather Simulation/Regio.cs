using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Simulation
{
    internal class Regio
    {
        public string evszak { get; private set; }
        public string nev { get; private set; }
        public DateTime datum { get; private set; }
        private int honap = 0;
        
        public Szel szel { get; private set; }

        private int legvaloszinubbSzam2 { get; set; }
        private int legvaloszinubbSzam { get; set; }


        private List<string> LehetsegesNevek = new List<string>() {
            "Észak-Magyarország", 
            "Észak-Alföld",
            "Dél-Alföld",
            "Pest",
            "Budapest",
            "Közép-Dunántúl",
            "Nyugat-Dunántúl",
            "Dél-Dunántúl"
        };
        


        public Regio(string Nev, DateTime Datum, int mostLikelySzelIndex, int mostLikelySzam2)
        {
            nev = Nev;
            int talalt = 0;
            for (int i = 0; i < LehetsegesNevek.Count; i++)
            {
                if (LehetsegesNevek[i].ToLower() == Nev.ToLower())
                {
                    talalt = 1;
                }
            }
            if (talalt == 0)
            {
                throw new Exception("Nem lehetséges régiónév!");
            }
            honap = Datum.Month;
            Evszakmentes();
            legvaloszinubbSzam = mostLikelySzelIndex;
            legvaloszinubbSzam2 = mostLikelySzam2;
            szel = new Szel(legvaloszinubbSzam,legvaloszinubbSzam2);


        }


        public void Evszakmentes()
        {
            if (honap >= 1 && honap <= 2 || honap == 12)
            {
                evszak = "Tél";
            }
            else if (honap >= 3 && honap <= 5)
            {
                evszak = "Tavasz";
            }
            else if(honap >= 6 && honap <= 8)
            {
                evszak = "Nyár";
            }
            else if(honap >= 9 && honap <= 11)
            {
                evszak = "Ősz";
            }
        }
        
        public void UjLegvaloszinubbSzelirany(int szam)
        {
            if (szel.Szeliranyok.Count - 1 < szam || szam > -1)
            {
                throw new Exception("Nem található szélirány a listában!");
            }
            else
            {
                legvaloszinubbSzam = szam;

            }
        }
        public void UjLegvaloszinubbSzam2(int szam)
        {
            if (0 < szam || szam > 99)
            {
                throw new Exception("Maximum 0 és 99 között generálható véletlenszerű szám!");
            }
            else
            {
                legvaloszinubbSzam2 = szam;

            }
        }

        public void napvaltas()
        {
            datum.AddDays(1);
            honap = datum.Month;

            szel = new Szel(legvaloszinubbSzam, legvaloszinubbSzam2);
        }

        public override string ToString()
        {
            return $" ││    Régió - {this.nev}   ││\n\n       Szél : {this.szel}";
        }
    }
}

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
        


        public Regio(string Nev, DateTime Datum)
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

            szel = new Szel();

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
        
        public void napvaltas()
        {
            datum.AddDays(1);
            honap = datum.Month;

            szel = new Szel();
        }
    }
}

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
        private List<szel> LehetsegesSzelTipusok { get; set; } = new List<szel>();
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
            if (LehetsegesNevek.Contains(nev))
            {
                nev = Nev;
            }
            else
            {
                throw new Exception($"Nem lehetséges régió név!");

            }
            honap = Datum.Month;
            Evszakmentes();


        }



        public void SzeltipusokFelvetele(szel Szél)
        {
            LehetsegesSzelTipusok.Add(Szél);

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
        }
    }
}

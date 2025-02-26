using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Simulation
{
    internal class Regio
    {
        public string nev { get; private set; }
        public DateTime datum { get; private set; }
        private int honap = 0;
        
        public Szel szel { get; private set; }

        private string Tipus { get; set; }
        private int evszak { get; set; }
        public Csapadek csapadek { get;private set; }
        public int paratartalom { get; private set; }
        private int legvaloszinubbSzam2 { get; set; }
        private int legvaloszinubbSzam { get; set; }

        public Katasztrofa katasztrofa { get;private set; }

        public double homerseklet { get;private set; }

        private List<string> Tipusok = new List<string>()
        {
            "szeles",
            "felhős",
            "napos",
            "esős",
            "viharos",
            "havazás"
        };

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
            
            datum = Datum; 
            honap = Datum.Month;

            legvaloszinubbSzam = mostLikelySzelIndex;
            legvaloszinubbSzam2 = mostLikelySzam2;

            szel = new Szel(legvaloszinubbSzam,legvaloszinubbSzam2);
            homerseklet = randomHomerseklet();
            paratartalom = randomParatartalom();
       

        }



        public Katasztrofa randomKatasztrofa(List<Katasztrofa> katasztrofak, int randomszam)
        {
            int evszak = 0;
            if (datum.Month < 3 || datum.Month == 12) {
                evszak = 1;
            }
            else if(datum.Month > 2 && datum.Month < 6)
            {
                evszak = 2;
            }
            else if(datum.Month > 5 && datum.Month < 9)
            {
                evszak = 3;
            }
            else
            {
                evszak = 4;
            }


            Random random = new Random();
            int szam = random.Next(0, 10000);
            int esely = Convert.ToInt32(katasztrofak[randomszam].Esely * 100.0 );
            if (szam <= esely && (katasztrofak[randomszam].eselyesevszak == evszak || katasztrofak[randomszam].eselyesevszak == 5))
            {
                return katasztrofak[randomszam];
            }
            else
            {
                return new Katasztrofa("Nem volt",5,0, 0);
            }

            
        }

        public string Tipusbeallitas(Csapadek csapadek)
        {
            string tipus = "";
            Random random = new Random();
            
            if (csapadek.mennyiseg == 0.0)
            {
                if (szel.szelsebesseg >= 36)
                {
                    tipus = "Szeles";
                }
                else if (random.Next(1) == 0)
                {
                    tipus = "Felhős";
                }
                else
                {
                    tipus = "Napos";
                }
            }
            else if(csapadek.Hoformaju)
            {
                tipus = "Havazás";
            }
            else
            {
                tipus = "Esős";
            }
            Tipus = tipus;
            return tipus;
        }
        private double randomHomerseklet()
        {
            Random random = new Random();
            switch (honap)
            {
                case 1:
                    if (random.Next(5) == 0)
                    {
                        homerseklet = random.Next(100, 130);

                    }
                    else
                    {
                        homerseklet = random.Next(30, 80);
                    }
                    break;
                case 2:
                    if (random.Next(5) == 0)
                    {
                        homerseklet = random.Next(100, 130);
                    }
                    else
                    {
                        homerseklet = random.Next(30, 80);
                    }
                    break;
                case 3:
                    homerseklet = random.Next(100,180);
                    break;
                case 4:
                    homerseklet = random.Next(120,230);
                    break;
                case 5:
                    homerseklet = random.Next(180, 260);
                    break;
                case 6:
                    homerseklet = random.Next(220,290);
                    break;
                case 7:
                    if (random.Next(10) == 0)
                    {
                        homerseklet = random.Next(230, 250);
                    }
                    else
                    {
                    homerseklet = random.Next(280,350);
                    }
                    break;
                case 8:
                    if (random.Next(5) == 0)
                    {
                        homerseklet = random.Next(190, 230);
                    }
                    else
                    {
                        homerseklet = random.Next(230, 280);
                    }
                    break;
                case 9:
                    {
                        if (random.Next(5) == 0)
                        {
                            homerseklet = random.Next(180, 230);
                        }
                        else
                        {
                            homerseklet = random.Next(230, 270);
                        }
                        break;
                    }
                case 10:
                        homerseklet = random.Next(160, 230);
                    break;
                case 11:
                    homerseklet = random.Next(120,170);
                    break;
                case 12:
                    if (random.Next(3) == 0)
                    {
                        homerseklet = random.Next(120, 140);
                    }
                    else
                    {
                        homerseklet = random.Next(40, 120);
                    }
                    break;
            }
            return homerseklet / 10.0;
        }
 

        private int randomParatartalom()
        {
            Random random = new Random();
            switch (honap)
            {
                case 1:
                    paratartalom = random.Next(30, 70);
                    break;
                case 2:
                    paratartalom = random.Next(30, 75);
                    break;
                case 3:
                    paratartalom = random.Next(30, 80);
                    break;
                case 4:
                    paratartalom = random.Next(40,80);
                    break;
                case 5:
                    paratartalom = random.Next(40, 80);
                    break;
                case 6:
                    paratartalom = random.Next(40,85);
                    break;
                case 7:
                    paratartalom = random.Next(40, 85);
                    break;
                case 8:
                    paratartalom = random.Next(40, 85);
                    break;
                case 9:
                    paratartalom = random.Next(40, 80);
                    break;
                case 10:
                    paratartalom = random.Next(30, 80);
                    break;
                case 11:
                    paratartalom = random.Next(30, 50);
                    break;
                case 12:
                    paratartalom = random.Next(30, 50);
                    break;
            }
            return paratartalom;
        }  
        public void UjLegvaloszinubbSzelirany(int szam)
        {
            if (szel.Szeliranyok.Count - 1 < szam || szam < 0)
            {
                Console.WriteLine(szam);
                throw new Exception("Nem található szélirány a listában!");
            }
            else
            {
                legvaloszinubbSzam = szam;

            }
        }
        public void UjLegvaloszinubbSzam2(int szam)
        {
            if (0 > szam || szam > 99)
            {
                throw new Exception("Maximum 0 és 99 között generálható véletlenszerű szám!");
            }
            else
            {
                legvaloszinubbSzam2 = szam;

            }
        }

        public void napvaltas(DateTime Datum)
        {
            Datum.AddDays(1);
            honap = Datum.Month;

            datum = Datum;
            szel = new Szel(legvaloszinubbSzam, legvaloszinubbSzam2);
            homerseklet = randomHomerseklet();
            paratartalom = randomParatartalom();
        }

        public override string ToString()
        {
            string elvalaszto = "──────────────────────────────────────────────────────";
            return $"         ==    Régió - {this.nev}    ==\n{elvalaszto}\n\n    Típus: \t\t\t\t{Tipus}\n\n    Szél : \n{this.szel} \n\n    Napi Átlaghőmérséklet : \t\t{this.homerseklet} °C\n\n    Páratartalom : \t\t\t{this.paratartalom} %";
        }
    }
}

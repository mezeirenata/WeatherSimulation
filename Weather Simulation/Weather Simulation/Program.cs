using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Weather_Simulation
{
 


        /// mezőgazd 3
        /// közleked 4
        /// funkciók függvények   -> pl hőmérséklet szélsőértékek megadása  
        ///                           pl új csapadék felvétele 
        ///                           pl régió felvétele
                   
    internal class Program
    {
        static string[] menu = new string[] {
        "Kilépés",
        "Napi időjárás jelentés", 
        "Közlekedés - események", 
        "Átlépés a következő napra",
        "Beállítások",/// ->> katasztrófa esemény felvétele
                      /// mezőgazdasági esemény felvétele   legvalószínűbb hónap, esélyek, milyen időjárási tényezőktől
                      /// közlekedés esemény felvétele -,,-
                      /// Vissza                    
        };

        static string[] beallitasok = new string[]
        {
            "Katasztrófa esemény felvétele",
            "Dátum beállítása",
            "Régió felvétele",
            "Vissza"
        };
        static List<Regio> Regiok = new List<Regio>();
        static List<Csapadek> Csapadekok = new List<Csapadek>();
        static List<Katasztrofa> Katasztrofak = new List<Katasztrofa>();

        static List<Csapadek> GeneraltCsapadekok = new List<Csapadek>();
            static void Main(string[] args)
        {
            DateTime dateofDay = DateTime.Now;
            int honap = dateofDay.Month;
            ///////
            CsapadekFeltoltes(dateofDay);
            KatasztrofaFeltoltes();
            Regiogeneralas(dateofDay);
            ///////
            Random random = new Random();
            Csapadek csapadek = new Csapadek("Hóesés", true,1, 0.0);
            Katasztrofa katasztrofa = new Katasztrofa("default",1,0, 0.0);
            ///////
            string elvalaszto = "──────────────────────────────────────────────────────";
            int randomIndex = random.Next(0,Csapadekok.Count);
            int choice = 1;
            int vegigmegy = 0;
            int streak = 0;
            ///////

            while (choice > 0)
            {
                choice = Menü(menu, dateofDay, "Menü");
                switch (choice) { 

                    default:
                        Console.Clear();
                        break;
                    case 1:
                        Console.Clear();
                        Header(dateofDay, "Régiók");
                        ////
                        ////
                        int regiochoice = 1;
                        while (regiochoice > -1)
                        {
                            regiochoice = RegioMenu(Regiok, dateofDay);
                            if (regiochoice < 0)
                            {
                                choice = -1;
                                vegigmegy++;
                                break;
                            }
                            else
                            {
                                try
                                {
                                    if (vegigmegy == 0)
                                    {
                                        int KatasztrofaIndex = random.Next(0,Katasztrofak.Count);
                                        for (int j= 0; j < Regiok.Count; j++)
                                        {
                                            csapadek = Csapadekgeneralas(dateofDay.Month, randomIndex, j);
                                            GeneraltCsapadekok.Add(csapadek);
                                            Regiok[j].Tipusbeallitas(csapadek);
                                            if (j == random.Next(0, Regiok.Count))
                                            {
                                                katasztrofa = Regiok[j].randomKatasztrofa(Katasztrofak, KatasztrofaIndex);
                                                if (katasztrofa.nev.ToLower() == "árvíz")
                                                {
                                                    streak++;
                                                }
                                                if (streak > 0 && streak < random.Next(5))
                                                {
                                                    int indexfind = 0;
                                                    for (int u = 0; u < Katasztrofak.Count; u++)
                                                    {
                                                        if (Katasztrofak[u].nev == "Árvíz")
                                                        {
                                                            indexfind = u;

                                                        }
                                                    }
                                                    katasztrofa = Regiok[j].randomKatasztrofa(Katasztrofak, indexfind);

                                                    streak++;
                                                }
                                            }
                                            else
                                            {
                                                katasztrofa = new Katasztrofa("Nem volt", 5, 0, 0.0);
                                            }
                                        }
                                    }
                                    RegioKiir(regiochoice, dateofDay, randomIndex, GeneraltCsapadekok, katasztrofa);
                                }
                                catch (Exception e)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(e.Message);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.ReadKey(true);
                                }
                                vegigmegy++;
                                break;
                            }
                        }
 
                        break;
                    case 2: /// Közlekedés ugyanaz mint mezőg.
                        Console.Clear();
                        Header(dateofDay, "Közlekedés - események");
                        vegigmegy++;
                        /// régió kilistázás
                        Console.ReadKey(true);
                        break;
    
                    case 3:
                        GeneraltCsapadekok.Clear();
                        randomIndex = random.Next(0, Csapadekok.Count);
                        dateofDay = dateofDay.AddDays(1);
                        Generalas(dateofDay);
                        honap = dateofDay.Month;
                        vegigmegy = 0;
                        break;
                    case 4:
                        vegigmegy++;
                        Header(dateofDay, "Beállítások");
                        int choice_setting = 1;
                        while (choice_setting > -1)
                        {
                            choice_setting = Menü(beallitasok,dateofDay, "Beállítások");
                            switch (choice_setting)
                            {
                                case 0: /// Katasztrófa esemény felvétele
                                    int kesz = 0;
                                    while (kesz == 0)
                                    {
                                    try
                                    {
                                    
                                    Console.Clear();
                                    Header(dateofDay,"Katasztrófa felvétele");
                                    Console.WriteLine(elvalaszto);
                                    Console.Write("   Katasztrófa esemény neve:\t\t");
                                    string nev = Console.ReadLine();
                                    if(nev == "")
                                    {
                                        throw new Exception("   Ne hagyja üresen!");
                                    }
                                       
                                    Console.Write("   Esélyes évszakok:\n\t   1 - Tél\n\t   2 - Tavasz\n\t   3 - Nyár\n\t   4 - Ősz\n\t   5 - Összes évszak\t\t");
                                    string evszak_ = Console.ReadLine();
                                            int evszak = 0;
                                            if (evszak_ == "")
                                            {
                                                throw new Exception("   Ne hagyja üresen!");
                                            }
                                            else
                                            {
                                                evszak = Convert.ToInt32(evszak_);
                                            }
                                    
                                    Console.Write("   Maximum halálesetek száma:\t\t");
                                    string halalesetek_ = Console.ReadLine();
                                            int halalesetek = 0;
                                            if (halalesetek_ == "")
                                            {
                                                throw new Exception("   Ne hagyja üresen!");
                                            }
                                            else
                                            {
                                                halalesetek = Convert.ToInt32(halalesetek_);
                                            }
                                    Console.Write("   Történés esélyei (%):\t\t");
                                    string esely_ = Console.ReadLine();
                                            double esely = 0.0;    
                                            if (esely_ == "")
                                            {
                                                throw new Exception("   Ne hagyja üresen!");
                                            }
                                            else
                                            {
                                                esely = Convert.ToDouble(esely_);
                                            }
                                    Katasztrofa katasztrofa_ = new Katasztrofa(nev, evszak,halalesetek,esely);
                                    Katasztrofak.Add(katasztrofa);

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\n   Sikeres katasztrófa feljegyzés!");
                                    Console.ReadKey(true);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    kesz = 1;
                                    }
                                    catch(Exception e)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(e.Message);
                                        Console.ReadKey(true);
                                        Console.ForegroundColor= ConsoleColor.White;
                                    }

                                    }
                                    break;
                                case 1: /// Dátum beállítása
                                    bool Beallitva = false;
                                    while (Beallitva == false)
                                    {
                                        Header(dateofDay, "Dátum");
                                        try
                                        {
                                            int ev_ellenorzott = 0;
                                            int honap_ellenorzott = 0;
                                            int nap_ellenorzott = 0;

                                            Console.WriteLine(elvalaszto);
                                            Console.Write("   Év:\t\t");
                                            string _ev = Console.ReadLine();
                                            if (_ev.Length != 4)
                                            {
                                                throw new Exception("Helytelen formában megadott év!");

                                            }
                                            else
                                            {
                                                ev_ellenorzott = Convert.ToInt32(_ev);
                                            }

                                            Console.Write("   Hónap:\t");
                                            string _honap = Console.ReadLine();
                                            if (_honap.Length != 2 && _honap.Length != 1)
                                            {
                                                throw new Exception("Helytelen formában megadott hónap!");

                                            }
                                            else if (Convert.ToInt32(_honap) > 12 || Convert.ToInt32(_honap) < 1)
                                            {
                                                throw new Exception("Hónapot csak 13-nál kisebb, és 0-nál nagyobb számmal lehet megadni!");
                                            }
                                            else
                                            {
                                                honap_ellenorzott = Convert.ToInt32(_honap);
                                            }

                                            Console.Write("   Nap:\t\t");
                                            int _nap = Convert.ToInt32(Console.ReadLine());
                                            int napszam = hanyNapjaLehet(ev_ellenorzott, honap_ellenorzott);
                                           
                                            if (_nap > napszam)
                                            {
                                                throw new Exception("Nincs ennyi napja a megadott hónapnak");
                                            }
                                            else if(_nap < 1)
                                            {
                                                throw new Exception("Nem létezik ilyen napja a hónapnak.");
                                            }
                                            else
                                            {
                                                nap_ellenorzott = _nap;
                                            }

                                            dateofDay = new DateTime(ev_ellenorzott, honap_ellenorzott, nap_ellenorzott, dateofDay.Hour, dateofDay.Minute, dateofDay.Second);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine();
                                            Console.WriteLine($"   Új dátum beállítva: {dateofDay} !");
                                            Console.ForegroundColor = ConsoleColor.White;

                                            Console.ReadKey(true);
                                            break;
                                        }
                                        catch (Exception e)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine();
                                            Console.WriteLine("   " + e.Message);
                                            Console.ForegroundColor = ConsoleColor.White;

                                            Console.ReadKey(true);
                                            Console.Clear();
                                        }

                                    }
                                    break;
                                default: /// Vissza
                                    choice_setting = -1;
                                    break;
                                case 2:
                                    Console.Clear();
                                    Header(dateofDay, "Régió felvétele");
                                    Console.ReadKey(true);
                                    break;
                             
                            }

                        }
                        break;
                }

            }
        }



        static void KatasztrofaFeltoltes()
        {
            try
            {
          
                Katasztrofa katasztrofa = new Katasztrofa("Földrengés",5, 115,1.25);
                Katasztrofak.Add(katasztrofa);
                katasztrofa = new Katasztrofa("Árvíz",5, 15,5.25);
                Katasztrofak.Add(katasztrofa);
                katasztrofa = new Katasztrofa("Erdőtűz",3, 5,15.25);
                Katasztrofak.Add(katasztrofa);

            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static Csapadek Csapadekgeneralas(int honap, int csapadekIndex, int sorszam)
        {

            string evszak = "";
            int csapadekEselyek = 0;
            if (honap == 12 || honap >= 1 && honap <= 2)
            {
                csapadekEselyek = 45;
                evszak = "tél";
            }
            if (honap >= 3 && honap <= 5)
            {
                csapadekEselyek = 65;
                evszak = "tavasz";
            }
            if (honap >= 6 && honap <= 8)
            {
                csapadekEselyek = 5;
                evszak = "nyár";
            }
            if (honap >= 9 && honap <= 10)
            {
                csapadekEselyek = 25;
                evszak = "ősz";
            }
            if (honap == 11)
            {
                csapadekEselyek = 35;
                evszak = "ősz";
            }

                double mennyiseg = 0.0;
            Random random = new Random();
            int randomszam = random.Next(0, 99);
            if (randomszam <= csapadekEselyek)
            {
                if (evszak == "tél")
                {
                    int mennyisegEsely = random.Next(0, 100);
                    if (mennyisegEsely < 5)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(100, 230);

                    }
                    if (mennyisegEsely == 6)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(230, 800);
                    }
                    if (mennyisegEsely > 6 || mennyisegEsely == 5)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(0, 800);
                    }

                }
                if (evszak == "tavasz")
                {
          
                    int mennyisegEsely = random.Next(0, 100);
                    if (mennyisegEsely < 5)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(100, 230);

                    }
                    if (mennyisegEsely == 6)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(300, 800);
                    }
                    if (mennyisegEsely > 6 || mennyisegEsely == 5)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(0, 800);
                    }
                }
                if (evszak == "ősz")
                {
                    int mennyisegEsely = random.Next(0, 100);
                    if (mennyisegEsely < 5)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(100, 230);

                    }
                    if (mennyisegEsely == 6)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(500, 800);
                    }
                    if (mennyisegEsely > 6 || mennyisegEsely == 5)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(0, 1000);
                    }
                }
                if (evszak == "nyár")
                {
                    int mennyisegEsely = random.Next(0, 100);
                    if (mennyisegEsely < 5)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(10, 50);

                    }
                    if (mennyisegEsely == 6)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(200, 800);
                    }
                    if (mennyisegEsely > 6 || mennyisegEsely == 5)
                    {
                        mennyiseg = Csapadekok[csapadekIndex].randomMennyiseg(0, 10);
                    }
                }

            }

         
            string  nev = Csapadekok[csapadekIndex].Csapadekforma;
            bool  havas = Csapadekok[csapadekIndex].Hoformaju;

               
            Csapadek csapadek = new Csapadek(nev, havas, sorszam, mennyiseg);

            return csapadek;

        }
        static void CsapadekFeltoltes(DateTime dateofDay)
        {
            int honap = dateofDay.Month;
            string evszak = "";
            if (honap == 12 ||( honap >= 1 && honap <= 2))
            {
                evszak = "tél";
            }
            if (honap >= 3 && honap <= 5)
            {
                evszak = "tavasz";
            }
            if (honap >= 6 && honap <= 8)
            {
                evszak = "nyár";
            }
            if (honap >= 9 && honap <= 11)
            {
                evszak = "ősz";
            }

            try
            {
                if (evszak == "tél")
                {
                Csapadekok.Clear();
                Csapadek csapadek = new Csapadek("Havazás",true,1,0.0);
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Eső",false,2,0.0);
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Jégdara", false, 3, 0.0);
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Jégeső", false, 4, 0.0);
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Ónos eső",false, 5, 0.0);
                Csapadekok.Add(csapadek);
                } 
                if (evszak == "tavasz")
                {
                Csapadekok.Clear();
                Csapadek csapadek = new Csapadek("Szitálás",false, 1, 0.0);
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Eső",false, 2, 0.0);
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Záporos csapadék", false, 3, 0.0);
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Jégeső", false, 4, 0.0);
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Ónos eső", false, 5, 0.0);
                Csapadekok.Add(csapadek);
                }
                if (evszak == "nyár")
                {
                    Csapadekok.Clear();
                    Csapadek csapadek = new Csapadek("Szitálás", false, 1, 0.0);
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Eső", false, 2, 0.0);
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Záporos csapadék", false, 3, 0.0);
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Ónos eső", false, 4, 0.0);
                    Csapadekok.Add(csapadek);
                }
                if (evszak == "ősz")
                {
                    Csapadekok.Clear();
                    Csapadek csapadek = new Csapadek("Eső", false, 1, 0.0);
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Záporos csapadék", false, 2, 0.0);
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Jégeső", false, 3, 0.0);
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Ónos eső", false, 4, 0.0);
                    Csapadekok.Add(csapadek);
                }
            }
            catch (Exception e) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

     
        static void Regiogeneralas(DateTime dateofDay)
        {
            try
            {
                Random random = new Random();
                int mostLikely = random.Next(0,8);
                int mostLikely2 = random.Next(0, 99);
                Regio regio = new Regio("Észak-Magyarország", dateofDay, mostLikely, mostLikely2);
                Regiok.Add(regio);
                regio = new Regio("Észak-Alföld", dateofDay, mostLikely, mostLikely2);
                Regiok.Add(regio);
                regio = new Regio("Dél-Alföld", dateofDay, mostLikely, mostLikely2);
                Regiok.Add(regio);
                regio = new Regio("Pest", dateofDay, mostLikely, mostLikely2);
                Regiok.Add(regio);
                regio = new Regio("Budapest", dateofDay, mostLikely, mostLikely2);
                Regiok.Add(regio);
                regio = new Regio("Közép-Dunántúl", dateofDay, mostLikely, mostLikely2);
                Regiok.Add(regio);
                regio = new Regio("Nyugat-Dunántúl", dateofDay, mostLikely, mostLikely2);
                Regiok.Add(regio);
                regio = new Regio("Dél-Dunántúl", dateofDay, mostLikely, mostLikely2);
                Regiok.Add(regio);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }


        }
      
        static void Generalas(DateTime dateofDay)
        {
            Random random = new Random();
            int mostLikely = random.Next(0, Regiok[0].szel.Szeliranyok.Count);
            int mostLikely2 = random.Next(0, 99);
            for (int i = 0; i < Regiok.Count; i++)
            {
                Regiok[i].napvaltas();
                try { 
                Regiok[i].UjLegvaloszinubbSzelirany(mostLikely);
                Regiok[i].UjLegvaloszinubbSzam2(mostLikely2);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            CsapadekFeltoltes(dateofDay);

        }

        static void RegioKiir(int regiochoice,DateTime dateofDay,int indexofCsapadek, List<Csapadek> generaltCsapadekok, Katasztrofa katasztrofa)
        {
            if (regiochoice == Regiok.Count)
            {
                return;
            }
            if (regiochoice > Regiok.Count || regiochoice < 0) {
                throw new Exception("Nem létezik ilyen régió.");
            }
            else
            {
            Regio regio = Regiok[regiochoice];
                Console.Clear();
                Header(dateofDay, "Régió statisztikák");
                Console.WriteLine(regiochoice);
                Console.WriteLine(generaltCsapadekok.Count);

                Console.WriteLine(regio);
                Console.WriteLine(
                    generaltCsapadekok[regiochoice] 
                    );
                Console.WriteLine(katasztrofa);
                Console.ReadKey(true);
                return;
            }
        }


        static void Header(DateTime dateofDay, string szoveg)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($" {szoveg} | {dateofDay}| Időjárás jelentés ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        static int hanyNapjaLehet(int ev, int honap)
        {
            int nap = 0;

            if (ev % 4 == 0 && honap == 2)
            {
                nap = 28;
            }
            else if(honap == 2)
            {
                nap = 29;
            }
            else if(honap == 4 || honap == 11 || honap == 9 || honap == 6)
            {
                nap = 30;
            }
            else
            {
                nap = 31;
            }

            return nap;
        }
        static int RegioMenu(List<Regio> menu, DateTime dateofDay)
        {
            int Current = 0;
            ConsoleKey k;
            do
            {
                Console.Clear();
                Header(dateofDay, "Régiók");
                for (int i = 0; i < menu.Count+1; i++)
                {
                    if (Current == i)
                    { 
                        if (i == menu.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($" -    Vissza ");
                            Console.SetCursorPosition(35, i + 2);
                            Console.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($" -    {menu[i].nev} ");
                        Console.SetCursorPosition(35, i + 2);
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.White;

                        }
                    }
                    else
                    {
                        if (i == menu.Count)
                        {
                        
                            Console.WriteLine($" -    Vissza ");
                            Console.SetCursorPosition(35, i + 2);
                            Console.WriteLine("");
                        }
                        else
                        {
                        Console.WriteLine($" -    {menu[i].nev}");
                        Console.SetCursorPosition(35, i + 2);
                        Console.WriteLine("");

                        }
                    }
                }
                k = Console.ReadKey(true).Key;
                if (k == ConsoleKey.UpArrow)
                {
                    Current--;
                    if (Current < 0)
                    {
                        Current = menu.Count;
                    }
                }
                else if (k == ConsoleKey.DownArrow)
                {
                    Current++;
                    if (Current == menu.Count + 1)
                    {
                        Current = 0;
                    }
                }

            } while (k != ConsoleKey.Enter && k != ConsoleKey.Escape);

            if (k == ConsoleKey.Escape)
            {
                Current = -1;
            }

            return Current;
        }
        
        static int Menü(string[] menu, DateTime dateofDay,string szoveg)
        {
            int Current = 0;
            ConsoleKey k;
            do
            {
                Console.Clear();
                Header(dateofDay, szoveg);
                for (int i = 0; i < menu.Length; i++)
                {
                    
                    if (Current == i)
                    {    
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($" ││    {menu[i]} ");
                        Console.SetCursorPosition(38,i+2);
                        Console.WriteLine($"   ││");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine($" ││    {menu[i]}");
                        Console.SetCursorPosition(38, i + 2);
                        Console.WriteLine($"   ││");
                    }
                }
                k = Console.ReadKey(true).Key;
                if (k == ConsoleKey.UpArrow)
                {
                    Current--;
                    if (Current < 0)
                    {
                        Current = menu.Length - 1;
                    }
                }
                else if (k == ConsoleKey.DownArrow)
                {
                    Current++;
                    if (Current == menu.Length)
                    {
                        Current = 0;
                    }
                }

            } while (k != ConsoleKey.Enter && k != ConsoleKey.Escape);

            if (k == ConsoleKey.Escape)
            { 
                Current = -1;
            }

            return Current;
        }
    }
}




//// év azért kezdődhet 0-val, mert mindenképpen 4 karakter hosszúságot kell megadni
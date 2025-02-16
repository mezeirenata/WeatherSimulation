using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Weather_Simulation
{
    internal class Program
    {
        static string[] menu = new string[] {
        "Kilépés",
        "Napi időjárás jelentés", 
        "Közlekedés - események", 
        "Mezőgazdaság - események",
        "Átlépés a következő napra",
        "Beállítások",/// ->> katasztrófa esemény felvétele
                      /// mezőgazdasági esemény felvétele   legvalószínűbb hónap, esélyek, milyen időjárási tényezőktől
                      /// közlekedés esemény felvétele -,,-
                      /// Vissza
        "Dátum beállítása"                      
        };

        static List<Regio> Regiok = new List<Regio>();
        static List<Csapadek> Csapadekok = new List<Csapadek>();
        static void Main(string[] args)
        {
            DateTime dateofDay = DateTime.Now;
            int honap = dateofDay.Month;
            CsapadekFeltoltes(dateofDay);
            Regiogeneralas(dateofDay);
            Random random = new Random();
            int randomIndex = random.Next(0, Csapadekok.Count);
            int choice = 1;
            while (choice > 0)
            {
                choice = Menü(menu, dateofDay);
                switch (choice) { 
                /// !!!!!!! Try block
                    default:
                        Console.Clear();
                        break;
                    case 1: 
                        Console.Clear();
                        Header(dateofDay);
                        ////
                        ////
                        int regiochoice = 1;
                        while (regiochoice > -1)
                        {
                            regiochoice = RegioMenu(Regiok, dateofDay);
                            if (regiochoice < 0)
                            {
                                choice = -1;
                                break;
                            }
                            else
                            {
                                try
                                { 
                                    RegioKiir(regiochoice, dateofDay, randomIndex);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.ReadLine();
                                }
                                break;
                            }
                        }
 
                        break;
                    case 2: /// Közlekedés ugyanaz mint mezőg.
                        Console.Clear();
                        Header(dateofDay);
                        /// régió kilistázás
                        Console.ReadLine();
                        break;
                    case 3: /// Mezőgazdaság
                        Console.Clear();
                        Header(dateofDay);
                        /// régió kilistázás NEM kell -> nem történik olyan sok esemény, viszont helyszínt kiírni
                        Console.ReadLine();
                        break;
                    case 4:
                        dateofDay = dateofDay.AddDays(1);
                        Generalas(dateofDay);
                        honap = dateofDay.Month;
                        randomIndex = random.Next(0, Csapadekok.Count);
                        break;
                    case 5:
                        Header(dateofDay);
                        Console.ReadLine();
                        break;
                    case 6:
                        Header(dateofDay);
                        Console.ReadLine();

                        break;
                }

            }
        }

        static string Csapadekgeneralas(int honap, int csapadekIndex)
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

            Random random = new Random();
            int randomszam = random.Next(0, 99);
            if (randomszam <= csapadekEselyek)
            {
                double mennyiseg = 0.0;
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
                if (mennyiseg == 0.0)
                {
                    return $"\n    Csapadék: \t\t\t0%";
                }
                return $"\n    Csapadék: \t\t\t\t{Csapadekok[csapadekIndex].Csapadekforma}\n    Napi csapadék mennyiség: \t\t{mennyiseg} mm ";
            }
            else
            {
                return $"\n    Csapadék: \t\t\t0%";
            }

        }
        static void CsapadekFeltoltes(DateTime dateofDay)
        {
            int honap = dateofDay.Month;
            string evszak = "";
            if (honap == 12 || honap >= 1 && honap <= 2)
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
                Csapadek csapadek = new Csapadek("Havazás");
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Eső");
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Hódara");
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Jégdara");
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Jégeső");
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Ónos eső");
                Csapadekok.Add(csapadek);
                } 
                if (evszak == "tavasz")
                {
                Csapadekok.Clear();
                Csapadek csapadek = new Csapadek("Szitálás");
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Eső");
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Záporos csapadék");
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Jégeső");
                Csapadekok.Add(csapadek);
                csapadek = new Csapadek("Ónos eső");
                Csapadekok.Add(csapadek);
                }
                if (evszak == "nyár")
                {
                    Csapadekok.Clear();
                    Csapadek csapadek = new Csapadek("Szitálás");
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Eső");
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Záporos csapadék");
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Ónos eső");
                    Csapadekok.Add(csapadek);
                }
                if (evszak == "ősz")
                {
                    Csapadekok.Clear();
                    Csapadek csapadek = new Csapadek("Eső");
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Záporos csapadék");
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Jégeső");
                    Csapadekok.Add(csapadek);
                    csapadek = new Csapadek("Ónos eső");
                    Csapadekok.Add(csapadek);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            // maximális mennyiség dátumtól függ
            // azon belül egy függvény megkapja a dátumot, és randomizál mennyiséget pl csapadek[randomszam].Randommennyiseg();
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
                Console.WriteLine(e.Message);
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
                    Console.WriteLine(e.Message);
                }
            }

            CsapadekFeltoltes(dateofDay);

        }

        static void RegioKiir(int regiochoice,DateTime dateofDay,int indexofCsapadek)
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
                Header(dateofDay);
                ////
                Console.WriteLine(regio);
                Console.WriteLine(
                    Csapadekgeneralas(dateofDay.Month, indexofCsapadek)
                    );
                Console.ReadLine();
                return;
            }
        }


        static void Header(DateTime dateofDay)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" Menü | {dateofDay}  | Időjárás jelentés ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }


        static int RegioMenu(List<Regio> menu, DateTime dateofDay)
        {
            int Current = 0;
            ConsoleKey k;
            do
            {
                Console.Clear();
                Header(dateofDay);
                for (int i = 0; i < menu.Count+1; i++)
                {
                    if (Current == i)
                    { 
                        if (i == menu.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($" -    Vissza ");
                            Console.SetCursorPosition(35, i + 2);
                            Console.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                        Console.ForegroundColor = ConsoleColor.Cyan;
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
        
        static int Menü(string[] menu, DateTime dateofDay)
        {
            int Current = 0;
            ConsoleKey k;
            do
            {
                Console.Clear();
                Header(dateofDay);
                for (int i = 0; i < menu.Length; i++)
                {
                    
                    if (Current == i)
                    {    
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($" ││    {menu[i]} ");
                        Console.SetCursorPosition(35,i+2);
                        Console.WriteLine($"   ││");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine($" ││    {menu[i]}");
                        Console.SetCursorPosition(35, i + 2);
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

/// órák?

/// esélyek szerint csinál időjárás típust, feltölti listába

/// zivatarok, természeti katasztrófák -> feltölteni évszak szerint listába, ebből randomizálhat
///                     ->ha nem történik aznap akkor ne írjon semmit (enum??)
///                     típus, esetleges szöveg hozzá, random jelentése



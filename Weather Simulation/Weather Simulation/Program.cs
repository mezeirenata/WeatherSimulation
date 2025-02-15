using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Weather_Simulation
{
    internal class Program
    {
        static string[] menu = new string[] {
        "Kilépés",
        "Napi időjárás jelentés", /// -> régiók kilistázása -> választási lehetőség + Vissza (enter)
        "Közlekedés - események", /// Vissza
        "Mezőgazdaság - események",/// Vissza
        "Átlépés a következő napra",
        "Beállítások" /// ->> katasztrófa esemény felvétele
                      /// mezőgazdasági esemény felvétele   legvalószínűbb hónap, esélyek, milyen időjárási tényezőktől
                      /// közlekedés esemény felvétele -,,-
                      /// Vissza
        };

        static List<Regio> Regiok = new List<Regio>();
        static void Main(string[] args)
        {
            DateTime dateofDay = DateTime.Now;
            Regiogeneralas(dateofDay);
            ///SzelLetrehozas(dateofDay); 
            ///Létrehozza az összes szelet, feltölti a régiók szeles listájába
            ///A szélből lehet randomizálni dátum szerint, szélrandomizálás -> megkapja a dátumot, esélyeket generál
            ///
            int choice = 1;
            while (choice > 0)
            {
                choice = Menü(menu, dateofDay);
                switch (choice) { 
                /// !!!!!!! Try block
                    default:
                        Console.Clear();
                        break;
                    case 1: /// mai nap jelentése
                        Console.Clear();
                        Header(dateofDay);
                        //for (int i = 0; i < Regiok.Count; i++)
                        //{
                        //    Console.WriteLine($"{Regiok[i].nev}");
                        //}
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
                                    RegioKiir(regiochoice, dateofDay);
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
                        //Generalas(dateofDay);
                        break;
                    case 5:
                        Header(dateofDay);
                        Console.ReadLine();
                        break;
                }

            }
        }


        static void Regiogeneralas(DateTime dateofDay)
        {
            try
            {
                Random random = new Random();
                int mostLikely = random.Next(0,7);
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

            /// vegigmegy a listakon (régió)
            /// elsosorban meghivja a napvaltas() függvényt, 1 nappal megemeli a dátumot  régió
            /// 
            /// esetlegesen új évszak, új esélyek
            /// ugyanugy legeneral mindent
            /// /// regio.UjlegvaloszinubbSzelirany(randomizaltszam);
        }

        static void RegioKiir(int regiochoice,DateTime dateofDay)
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
                Console.WriteLine(regio);
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
///  régiók böngészése
///  előre legenerálás nap kezdésekor

        /// csapadék, időjárás, átlag hőmérséklet ( -> hőmérsékletek generálása),
        /// ellenőrzi a dátumot -> visszaad egy évszakot, esélyek szerint hozza létre a csapadék típusú objektumokat (példányosítás)
        /// esélyeknek megfelelő csapadékok vannak a listában, ezt feltölti a főosztályba, hogy abból válaszhasson
        /// 
        /// esélyek szerint csinál időjárás típust, feltölti listába
        /// 
        /// zivatarok, természeti katasztrófák -> feltölteni évszak szerint listába, ebből randomizálhat
        ///                     ->ha nem történik aznap akkor ne írjon semmit (enum??)
        ///                     típus, esetleges szöveg hozzá, random jelentése

        //// szél (iránya, sebessége)
        ///Többi Readme-n
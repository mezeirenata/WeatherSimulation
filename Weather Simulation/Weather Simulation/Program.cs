using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Weather_Simulation
{
    internal class Program
    {
        static string[] menu = new string[] {
        "Kilépés",
        "Napi időjárás jelentés", /// -> régiók kilistázása -> választási lehetőség
        "Közlekedés - események",
        "Mezőgazdaság - események",
        "Átlépés a következő napra"

        };

        static List<Regio> Regiok = new List<Regio>();
        static void Main(string[] args)
        {
            DateTime dateofDay = DateTime.Now;
            ElsoGeneralas(dateofDay);
            ///SzelLetrehozas(dateofDay); 
            ///Létrehozza az összes szelet, feltölti a régiók szeles listájába
            ///A szélből lehet randomizálni dátum szerint, szélrandomizálás -> megkapja a dátumot, esélyeket generál
            ///
            int choice = 1;
            while (choice > 0)
            {
                choice = Menü(menu, dateofDay);
                switch (choice)
                {
                    case -1: break;
                    case 0: break;
                    case 1:
                        Console.Clear();
                        /// régió kilistázás
                        Console.ReadLine();
                        break;
                    case 2:  break;
                    case 4:
                        dateofDay = dateofDay.AddDays(1);
                        Generalas(dateofDay);
                        break;

                }

            }
        }

        static void ElsoGeneralas(DateTime dateofDay)
        {
            Regio regio = new Regio("Észak-Magyarország", dateofDay);
            /// letrehozza
            /// feltolti listaba
        }

        static void Generalas()
        {

            /// vegigmegy a listakon
            /// elsosorban meghivja a napvaltas() függvényt, 1 nappal megemeli a dátumot
            /// esetlegesen új évszak, új esélyek
            /// ugyanugy legeneral mindent
        }


        static int Menü(string[] menu, DateTime dateofDay)
        {
            int Current = 0;
            ConsoleKey k;
            do
            {
                Console.Clear();

                Console.WriteLine($" Menü ||{dateofDay} || Időjárás jelentés ");
                Console.WriteLine();
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
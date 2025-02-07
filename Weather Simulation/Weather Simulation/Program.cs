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
        "Átlépés a következő napra"
        };
        static void Main(string[] args)
        {
            DateTime dateofDay = DateTime.Now;
            int choice = 1;
            while (choice > 0)
            {
                choice = Menü(menu, dateofDay);
                switch (choice)
                {
                    case 0: break;
                    case 1: //// régiók kilistázása
                    case 2:
                        dateofDay = dateofDay.AddDays(1);
                        break;

                }

            }
        }

        static int Menü(string[] menu, DateTime dateofDay)
        {
            int Current = 0;
            ConsoleKey k;
            do
            {
                Console.Clear();

                Console.WriteLine($" Menü ||{dateofDay}");
                Console.WriteLine();
                for (int i = 0; i < menu.Length; i++)
                {
                    if (Current == i)
                    {    
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($" ||    {menu[i]}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine($" ||    {menu[i]}");
                    }
                }

                k = Console.ReadKey().Key;
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

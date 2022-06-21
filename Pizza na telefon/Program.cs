using System;
using System.Threading;

namespace Pizza_na_telefon
{
    public static class GlobalData
    {
        public static int[] MenuPositions = new int[99];
    };
    public class Funtions
    {
        public void CustomConsoleWriteLine(string text, string  text_color = "white", bool is_centered = false)
        {
            switch (text_color)
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            
            if (is_centered)
            {
                Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine(text);
            }

            Console.ResetColor();
        }
    }

    public class Pizzeria
    {
        public Pizzeria()
        {
            var functions = new Funtions();

            functions.CustomConsoleWriteLine("Witaj w naszej Pizzeri Italiano", "green", true);
            functions.CustomConsoleWriteLine("Zapraszamy do złożenia zamówienia", "", true);
            //Thread.Sleep(2500); //przerwa w wykonywaniu kodu na 2.5s

            functions.CustomConsoleWriteLine("\nOto menu dla Ciebie", "", true);

            this.Menu();

            Console.WriteLine("\n\nAby zamknąć program w dowolnej chwili kliknij klaiwsz 'esc'");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                this.KoniecProgramu();
            }
        }
        public void Menu()
        {
            var functions = new Funtions();
            functions.CustomConsoleWriteLine("\n\nNapoje", "green", true); //środek
            functions.CustomConsoleWriteLine("\nCola\t\t7zł");

            for(int i=0; i<99; i++)
            {
                if(i == 0)
                {
                    functions.CustomConsoleWriteLine("\nCo zamawiasz (podaj numer pozycji)?");
                }
                else
                {
                    functions.CustomConsoleWriteLine("\nCzy chcesz zamówić coś jeszcze? ('0' => koniec składania zamówienia)");
                }
                var value = int.Parse(Console.ReadLine());
                if(value == 0)
                {
                    Console.WriteLine("czyszczenie + przejscie do zamowienia");
                    for (int j = 0; j <= GlobalData.MenuPositions.Length; j++)
                    {
                        if(GlobalData.MenuPositions[j] == 0)
                        {
                            continue;
                        }
                        Console.WriteLine("no to: " + GlobalData.MenuPositions[j]);
                    }
                    return;
                }
                GlobalData.MenuPositions[i] = value;
            }
        }

        public void KoniecProgramu()
        {
            Console.WriteLine("Koniec programu");
            return;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var functions = new Funtions();
            var pizzeria = new Pizzeria();
        }
    }
}

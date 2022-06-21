using System;
using System.Threading;

namespace Pizza_na_telefon
{
    public static class GlobalData
    {
        public static int[] MenuPositions = new int[99];
        public static string KlientImie;
        public static string KlientNazwisko;
        public static string KlientAdres;
        public static string KlientUwagiDoZamowienia;
        public static string KlientMetodaPlatnosci;
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

            for(int i=0; i<GlobalData.MenuPositions.Length; i++)
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
                    this.Szczegoly_Zamowienia();
                    return;
                }
                GlobalData.MenuPositions[i] = value;
            }
        }

        public void Szczegoly_Zamowienia()
        {
            Console.WriteLine("czyszczenie + przejscie do zamowienia' czyszczenie 3s, może jakiś loader?");
            Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine("Wprowadzanie danych osobowych, adresu, itp. bez walidacji");



            Console.WriteLine("Naciśnij dowolny klawisz aby przejśc do podsumowania");
            Console.ReadKey();
            this.Podsumowanie();
        }

        public void Podsumowanie()
        {
            Console.WriteLine("czyszczenie 3s, może jakiś loader?");
            Thread.Sleep(3000);
            Console.Clear();

            Console.WriteLine("Tutaj wypisujemy elementy zamówienia + dane osobowe. wszystko ładnie wyśietlone i wgl. Trzeba pomyśleć nad obiegktem elementów menu tak by można było je wywoływać po numerkach.");

            Console.WriteLine("Naciśnij dowolny klawisz aby przejśc do dalej");
            Console.ReadKey();
            this.ThankYouPage();
        }

        public void ThankYouPage()
        {
            Console.WriteLine("czyszczenie 3s, może jakiś loader?");
            Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine("Dziekujemy za złozenie zamowienia i wgl, moze damy tu jakies smieszne emotki? ");
            Console.WriteLine("𓁹‿𓁹");
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

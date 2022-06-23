using System;
using System.Threading;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Pizza_na_telefon
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
    public static class GlobalData
    {
        public static List<int> MenuPositions = new List<int>();
        public static string KlientImie;
        public static string KlientNazwisko;
        public static string KlientAdres;
        public static string KlientUwagiDoZamowienia;
        public static int KlientMetodaPlatnosci;
    };

    public static class Functions
    {
        public static string CustomConsoleWriteLine(string text, string text_color = "white", bool is_centered = false)
        {
            text_color = text_color.ToLower();

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
            return null;
        }
    }

    public class Pizzeria
    {
        public Pizzeria()
        {

            Functions.CustomConsoleWriteLine("Witaj w naszej Pizzeri Italiano", "green", true);
            Functions.CustomConsoleWriteLine("Zapraszamy do złożenia zamówienia", "", true);
            //Thread.Sleep(2500); //przerwa w wykonywaniu kodu na 2.5s
            Functions.CustomConsoleWriteLine("\nOto menu dla Ciebie", "", true);

            this.Menu();

            Console.WriteLine("\n\nAby zamknąć program w dowolnej chwili kliknij klaiwsz 'esc'");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                this.KoniecProgramu();
            }
        }

        public void Loader()
        {
            Console.Write("Ładowanie... //");
            for(int i=0; i<3; i++)
            {
                Console.Write("\\//\\");
                Thread.Sleep(850);
            }
        }
        public void Menu()
        {
            Console.WriteLine("\n\n");
            Functions.CustomConsoleWriteLine("Napoje", "green", true); //środek
            Console.WriteLine("\n");
            Functions.CustomConsoleWriteLine("1.Cola           \t7zł","", true);
            Functions.CustomConsoleWriteLine("2.Fanta          \t7zł","",true);
            Functions.CustomConsoleWriteLine("3.Sprite          \t7zł","",true);
            Functions.CustomConsoleWriteLine("4.Woda gazowana  \t7zł","",true);
            Functions.CustomConsoleWriteLine("5.Woda niegazowana\t7zł","",true);

            Console.WriteLine("\n\n");
            Functions.CustomConsoleWriteLine("Pizza", "green", true); //środek
            Console.WriteLine("\n");
            Functions.CustomConsoleWriteLine("6.Margherita \t\t18zł","", true);
            Functions.CustomConsoleWriteLine("7.Neapolitana\t\t22zł","", true);
            Functions.CustomConsoleWriteLine("8.Carbonara  \t\t25zł", "", true);
            Functions.CustomConsoleWriteLine("9.Mexico      \t30zł", "", true);
            Functions.CustomConsoleWriteLine("10.Rukola    \t\t35zł", "", true);
            Functions.CustomConsoleWriteLine("11.Hawaii    \t\t15zł", "", true);

            bool koniec_wprowadzania = false;
            do
            {
                if (GlobalData.MenuPositions.Count < 1) {
                    Functions.CustomConsoleWriteLine("\nCo zamawiasz (podaj numer pozycji)?" + GlobalData.MenuPositions.Count);
                }
                else
                {
                    Functions.CustomConsoleWriteLine("\nCzy chcesz zamówić coś jeszcze? ('0' => koniec składania zamówienia)");
                }

                var value = int.Parse(Console.ReadLine());

                if (value == 0)
                {
                    koniec_wprowadzania = true;
                    this.Szczegoly_Zamowienia();
                    return;
                }
                else
                {
                    GlobalData.MenuPositions.Add(value);
                }

            } while (koniec_wprowadzania == false);
        }

        public void Szczegoly_Zamowienia()
        {
            Console.WriteLine("czyszczenie + przejscie do zamowienia' czyszczenie 3s, może jakiś loader?");
            //Thread.Sleep(3000);
            Console.Clear();
            Functions.CustomConsoleWriteLine("Wprowadź swoje imie","Green", false);
            GlobalData.KlientImie = Console.ReadLine();
            Functions.CustomConsoleWriteLine("Wprowadź swoje nazwisko","Green", false);
            GlobalData.KlientNazwisko = Console.ReadLine();
            Functions.CustomConsoleWriteLine("Wprowadź miasto, ulice i numer domu/mieszkania","Green", false);
            GlobalData.KlientAdres = Console.ReadLine();
            Functions.CustomConsoleWriteLine("Uwagi do zamówienia", "Green", false);
            GlobalData.KlientUwagiDoZamowienia = Console.ReadLine();
            Functions.CustomConsoleWriteLine("Dostępne metody płatności:", "Green", false);
            Console.WriteLine("\n1.Karta płatnicza\n2.Płatność gotówką przy odbiorze");
            bool bledna_platnosc = false;
            do
            {
                Console.WriteLine("\nWybierz metode płatności");
                try
                {
                    GlobalData.KlientMetodaPlatnosci = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("To nie liczba byczq");
                }

                switch (GlobalData.KlientMetodaPlatnosci)
                {
                    case 1 :
                        Console.WriteLine("wybrana metoda płatności to płatność kartą");
                        bledna_platnosc = true;
                        break;
                    case 2:
                        Console.WriteLine("wybrana metoda płatności to płatność gotówką przy odbiorze");
                        bledna_platnosc = true;
                        break;
                    default:
                        Console.WriteLine("Wybrana metoda płatności nie istnieje");
                        break;
                }

            } while (bledna_platnosc == false);


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
            string fileName = "src/data/menu.json";
            List<Pizza> Menu = new List<Pizza>();
            //string menuJSON = File.ReadAllText(fileName);
            //Menu = JsonSerializer.Deserialize<List<Pizza>>(menuJSON);
            //var functions = new Funtions();
            //var pizzeria = new Pizzeria();
            Menu.Add(new Pizza
            {
                Id=1,
                Name="Kapa",
                Price=69420
            });
            Menu.Add(new Pizza
            {
                Id = 2,
                Name = "Kapaasdasd",
                Price = 6941220
            });

            string jsonString = JsonSerializer.Serialize(Menu);
            File.WriteAllText(fileName, jsonString);

            Console.WriteLine(File.ReadAllText(fileName)); */
        }
    }
}
